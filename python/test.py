# -*- coding: utf-8 -*-
import sys
import numpy as np
import cv2
import copy
# import dlib
from PIL import Image, ImageDraw
from sklearn import model_selection, svm, metrics
from sklearn.model_selection import train_test_split
import joblib
from os import listdir
from pynput.keyboard import Listener, Key
import pyautogui

# detector = dlib.get_frontal_face_detector()
ROOTDIR = "G1"
TYPE = "5"
ORIGIN = f"extracted/{ROOTDIR}/{TYPE}/"
ROOT = f"extracted/{ROOTDIR}/"
STARTNUM = 701
MAXNUM = 1000
CATEGORIES = ['0', '1', '2', '5']
CATEGORIES2 = ['0', '1', '2', '5', 'X']
width = 64
height = 64
EXCUTE = True
DURATION = 0.05
DELTA = 3
GAMMA = 600

def GetImage(img):
    temp = copy.deepcopy(img)
    # img_hsv = face_detector(temp)
    img_hsv = cv2.cvtColor(temp, cv2.COLOR_BGR2HSV)
    img_hsv = cv2.GaussianBlur(img_hsv, (5, 5), 0)
    result, img, mask, loca = GetHand(img_hsv, img)
    mask = cv2.resize(cv2.cvtColor(mask, cv2.COLOR_BGR2GRAY), (64,64))
    return result, img, mask, loca

# def GetRectImage(img):
#     temp = copy.deepcopy(img)
#     img_hsv = face_detector(temp)
#     img_hsv = cv2.cvtColor(img_hsv, cv2.COLOR_BGR2HSV)
#     img_hsv = cv2.GaussianBlur(img_hsv, (5, 5), 0)
#     result ,img, mask, loca = GetRectHand(img_hsv, img)
#     mask = cv2.resize(cv2.cvtColor(mask, cv2.COLOR_BGR2GRAY), (64,64))
#     return result, img, mask, loca

def GetHand(img_hsv, img):                  #이미지에서 손 추출
    low = np.array([3, 40, 85], dtype="uint8")      #색 하한
    high = np.array([14, 255, 255], dtype="uint8")  #색 상한
    mask = cv2.inRange(img_hsv, low, high)          #색 필터링

    kernelOpen = np.ones((5, 5))
    kernelClose = np.ones((15, 15))

    maskOpen = cv2.morphologyEx(mask, cv2.MORPH_OPEN, kernelOpen)           #외부 노이즈 제거
    maskClose = cv2.morphologyEx(maskOpen, cv2.MORPH_CLOSE, kernelClose)    #내부 노이즈 제거

    contours, hierarchy = cv2.findContours(maskClose, cv2.RETR_LIST, cv2.CHAIN_APPROX_SIMPLE)   #외곽선 검출

    if contours == []:
        return False, img, img, (0,0)

    largest = sorted(contours, key = cv2.contourArea)       #가장 큰 외곽선 검출
    x, y, width, height = cv2.boundingRect(largest[-1])     #외곽선 주변 범위 사각형으로 추출

    mask = np.zeros_like(img)                                           #손만 넣을 이미지 준비
    cv2.drawContours(mask, [largest[-1]], 0, (255, 255, 255), -1)       #손 부분만 흰색으로 처리
    gray = cv2.cvtColor(mask, cv2.COLOR_BGR2GRAY)                       #회색조 처리
    # gray = cv2.bitwise_not(gray)                                        #색 반전
    ret, thresh = cv2.threshold(gray, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)        #2진으로 변환
    distances = cv2.distanceTransform(thresh, cv2.DIST_L2, 5)           #거리에 따른 이미지로 변환
    result = cv2.normalize(distances, None, 255, 0, cv2.NORM_MINMAX, cv2.CV_8UC1)   #출력용 이미지로 변환(0~1 => 0~255로)
    _, _, _, maxloc = cv2.minMaxLoc(result)                    #중심 부분 추출
    mask[mask==255] = img[mask==255]                                    #손 부분 이미지만 추출
    mask=mask[y:y+height,x:x+width]                                     #사각형으로 자름
    cv2.drawContours(img, [largest[-1]], 0, (255, 255, 0), 3)           #이미지에 손 테두리 그림
    cv2.circle(img, maxloc, 3, (0,0,255), 3)                            #손 중심 추출

    return True, img, mask, maxloc

# def GetRectHand(img_hsv, img):
#     low = np.array([3, 49, 85], dtype="uint8")
#     high = np.array([15, 255, 255], dtype="uint8")
#     mask = cv2.inRange(img_hsv, low, high)
#
#     kernelOpen = np.ones((5, 5))
#     kernelClose = np.ones((15, 15))
#
#     maskOpen = cv2.morphologyEx(mask, cv2.MORPH_OPEN, kernelOpen)
#     maskClose = cv2.morphologyEx(maskOpen, cv2.MORPH_CLOSE, kernelClose)
#
#     contours, hierarchy = cv2.findContours(maskClose, cv2.RETR_LIST, cv2.CHAIN_APPROX_SIMPLE)
#
#     if contours == []:
#         return False, img
#
#     largest = sorted(contours, key = cv2.contourArea)
#     x, y, width, height = cv2.boundingRect(largest[-1])                 #손 주변 사각형 따기
#
#     # mask = np.zeros_like(img)
#     # cv2.drawContours(mask, [largest[-1]], 0, (255, 255, 255), -1)
#     mask = img[y:y+height,x:x+width]                                    #손 주위 자르기
#     # mask=mask[y:y+height,x:x+width]
#
#     # cv2.drawContours(img, [largest[-1]], 0, (255, 255, 0), 3)
#
#     return True, img, mask, {'x':x,'y':y, 'width':width, 'height':height}

# def face_detector(img, size = 0.5):
#     faces = detector(img)
#     if faces == []:
#         return img
#     for face in faces:
#         cv2.rectangle(img, (face.left(), face.top()), (face.right(), face.bottom()), (0, 0, 0), -1)
#     return img

def Get_ImageFile():                            #이미지 파일로 추출
    imageCnt = STARTNUM

    video = cv2.VideoCapture(0)
    video.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
    video.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

    while imageCnt <= MAXNUM:
    # while True:
        ret, frame = video.read()
        if ret:
            try:
                print(ORIGIN + str(imageCnt))
                # hand = GetImage(frame)
                result, img, hand, _ = GetImage(frame)
                # hand = cv2.resize(cv2.cvtColor(hand,cv2.COLOR_BGR2GRAY), (64,64)) # 이미지에서 손 이미지 추출
                if result:
                    writePath = f"{ORIGIN}{imageCnt}.png"  # 쓸 이미지 경로
                    cv2.imwrite(writePath, hand)
                    imageCnt += 1
                cv2.imshow("hand", hand)
            except:
                print("error")
        if cv2.waitKey(1) == ord('q'): break
    video.release()
    cv2.destroyAllWindows()
    return

# def Get_RectImageFile():
#     imageCnt = STARTNUM
#
#     video = cv2.VideoCapture(0)
#     video.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
#     video.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)
#
#     while imageCnt <= MAXNUM:
#     # while True:
#         ret, frame = video.read()
#         if ret:
#             try:
#                 print(ORIGIN + str(imageCnt))
#                 # hand = GetImage(frame)
#                 result, img, hand, _ = GetRectImage(frame)
#                 # hand = cv2.resize(cv2.cvtColor(hand,cv2.COLOR_BGR2GRAY), (64,64)) # 이미지에서 손 이미지 추출
#                 if result:
#                     writePath = f"{ORIGIN}{imageCnt}.png"  # 쓸 이미지 경로
#                     cv2.imwrite(writePath, hand)
#                     imageCnt += 1
#                 cv2.imshow("hand", hand)
#             except:
#                 print("error")
#         if cv2.waitKey(1) == ord('q'): break
#     video.release()
#     cv2.destroyAllWindows()
#     return

# def Get_Model():
#     X = []
#     Y = []
#
#     for idx, cat in enumerate(CATEGORIES2):
#         # label = [0 for i in range(len(CATEGORIES))]
#         # label[idx] = 1
#
#         for file in listdir(ROOT+cat):
#             filepath = f"{ROOT}{cat}/{file}"
#             print(filepath)
#             img = Image.open(filepath)
#             arr = np.array(img)
#             arr = np.ravel(arr, order='C')
#             X.append(arr/256)
#             Y.append(cat)
#
#     X = np.array(X)
#     Y = np.array(Y)
#
#     x_train, x_test, y_train, y_test = train_test_split(X, Y, test_size=0.3, random_state=0, shuffle=True, stratify=Y)
#
#     clf = svm.SVC()
#     clf.fit(x_train, y_train)
#     predict = clf.predict(x_test)
#
#     accuracy = metrics.accuracy_score(y_test, predict)
#     cl_report = metrics.classification_report(y_test, predict)
#     print(f"정답률={accuracy}")
#     print("리포트 = ")
#     print(cl_report)
#     joblib.dump(clf, 'model.pkl')

def Use_model(img, clf):                        #손 인식
    X = []

    arr = np.array(img)
    arr = arr.reshape((4096))
    X.append(arr / 256)
    predict = clf.predict(X)
    # print(predict)

    return predict

def Show_Hand(CameraNum, Sensitive):            #이미지 출력
    video = cv2.VideoCapture(CameraNum)
    video.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
    video.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)
    clf = joblib.load('model.pkl')

    scrX, scrY = pyautogui.size()

    listener = Listener(on_press=keypressed)
    listener.start()

    origin = ()
    pre_predict = "5"
    while EXCUTE:
        ret, frame = video.read()
        if ret:
            result, img, hand, loca = GetImage(frame)
            if result:
                predict = Use_model(hand, clf)
                cv2.rectangle(img, (0,0), (50, 35), (255,255,255), -1)
                cv2.putText(img, str(predict), (4, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 0, 255), 1, cv2.LINE_AA)
                origin, pre_predict = movemouse(origin, loca, (scrX, scrY), pre_predict, predict, Sensitive)
            cv2.imshow("hand", img)
        cv2.waitKey(1)
    video.release()
    cv2.destroyAllWindows()
    return

def movemouse(origin, loca, scrsize, pre_predict, predict, sensitvie):      #마우스 이동
    if loca == ():
        return (scrsize[0]/2, scrsize[1]/2), predict
    if origin == ():
        return loca, predict

    try:
        mouseX, mouseY = pyautogui.position()
        x = int(sensitvie*(loca[0] - origin[0]))
        y = int(sensitvie*(loca[1] - origin[1]))
        nextX = mouseX - x
        nextY = mouseY + y
    except:
        return loca, predict
        # print("error")

    # print(f"{pre_predict},{predict} ({x},{y}) ({nextX},{nextY})")

    if nextX <= 0 or nextX >= scrsize[0] or nextY <= 0 or nextY >= scrsize[1]:
        return loca, predict

    if predict == '5':
        # pyautogui.mouseUp(scrsize[0]/2, scrsize[1]/2)
        return loca, predict

    # elif pre_predict == '0' and predict == '1':
    #     pyautogui.mouseUp(nextX, nextY, button='left', duration = DURATION)
    # elif pre_predict == '2' and predict == '1':
    #     pyautogui.mouseUp(nextX, nextY, button='right', duration= DURATION)
    elif predict == '1':
        if abs(x) <=DELTA or abs(y) <= DELTA or abs(x) + abs(y) > GAMMA:
            return loca, predict
        pyautogui.moveTo(nextX, nextY)
    elif predict == '0':
        pyautogui.click(button='left')
    elif predict == '2':
        pyautogui.click(button='right')
    # elif pre_predict == '0' and predict == '0':
    #     pyautogui.mouseDown(nextX, nextX, button='left')
    # elif pre_predict == '2' and predict == '2':
    #     pyautogui.mouseDown(nextX, nextX, button='right')
    # elif pre_predict == '0' or '2' and predict == '0' or '2':
    #     pyautogui.mouseUp(nextX, nextX, button='left')

    # print(f"{mouseX},{mouseY} {x},{y}")
    return loca, predict

def keypressed(key):
    global EXCUTE
    if key == Key.f2:
        EXCUTE = False

# Get_RectImageFile()
# Get_ImageFile()
# Get_Model()

print(sys.argv)
try:
    Camera = int(sys.argv[1])
except:
    print("error1")
    Camera = 0
try:
    Sensitive = int(sys.argv[2])
except:
    print("error2")
    Sensitive = 3

Show_Hand(Camera, Sensitive)

""" 손가락 가장 높은 점 찾기
    hull = cv2.convexHull(largest[-1], returnPoints=False)
    defects = cv2.convexityDefects(largest[-1], hull)

    points = []
    # try:
    for i in range(defects.shape[0]):       #defects는 3차원, 1차원=개수 2차원=무쓸모 3차원=내부 값들
        s,e,f,_ = defects[i,0]              #s 시작 index, e 끝 index, f 가장 먼 index
        points.append(tuple(largest[-1][s][0]))
        # end = tuple(largest[-1][e][0])
        # far = tuple(largest[-1][f][0])
        # cv2.circle(img, start, 3, (0, 0, 255), 3)
        # cv2.circle(img, far, 3, (0, 0, 0), 2)
    points.sort(key=lambda x:x[1])
    cv2.circle(img, points[0], 3, (0, 0, 255), 3)
    # except:
    #     print("error")
"""