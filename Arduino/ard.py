import serial
import sqlite3
import time
import requests
from datetime import date

emailParam = ""
codeParam = ""
counter = 0

# idHistory, idBox, weightBox, temperatureBox, humidityBox, dateTimeHistory
def send(humidityBox, temperatureBox, weightBox):
    idBox = 17
    dateTime = date.today()
    url = "http://api.mgarage.com.ua/historybox/createhistory?id_box="+str(idBox)+"&Weight_box="+str(weightBox)+"&Temperature_box="+str(temperatureBox)+"&Humidity_box="+str(humidityBox);
    print(url);
    r = requests.get(url)
    # r = requests.post(url = API_ENDPOINT, data = data)
    pastebin_url = r.text
    print("The pastebin URL is:%s"%pastebin_url)



def read_data():
    arduinodata = serial.Serial('/dev/cu.usbserial-14210', 9600, timeout=0.5)
    while arduinodata.inWaiting:
        val = arduinodata.readline().decode('ascii')
        if len(val) > 4:
            return val


while 1:

    cod = read_data()
    if cod != "":
        x = cod.split(",")
        print(x)
        element1 = float(x[0])
        element2 = float(x[1])
        element3 = float(x[2])
        send(element1, element2, element3)




