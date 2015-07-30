#!/usr/bin/env python
from flask import Flask, render_template, request, redirect, session
from flaskext.mysql import MySQL
import os
import sys, time


mysql = MySQL()
app = Flask(__name__)
app.config['MYSQL_DATABASE_USER'] = 'serverstudy'
app.config['MYSQL_DATABASE_PASSWORD'] = 'serverstudy!@#'
app.config['MYSQL_DATABASE_DB'] = 'serverstudy'
app.config['MYSQL_DATABASE_HOST'] = 'data.khuhacker.com'
app.config['MYSQL_CHARSET'] = 'utf-8'
mysql.init_app(app)

app.secret_key = 'A0Zr98j/3yX R~XHH!jmN]LWX/,?RT'

@app.route('/')
def login():
    return render_template("index.html")

@app.route('/loginpage')
def loginpage():
    if'logged_in' in session.keys() and (session['logged_in'] == True):
        while(True):
            state = "use able"
            log = open("log.txt", "r")
            mtemp = log.readline()
            time.sleep(0.5)
            if os.path.exists("b" + str(mtemp) + ".txt"):
                log1 = open("b" + str(mtemp) + ".txt" , "r")
                for i in log1.readlines():
                    if(i == "0\r\n"):
                        state = "no"
                        log.close()
                        sef = open("log.txt", "w")
                        mtemp = int(mtemp) + 1
                        sef.writelines(str(mtemp))
                        sef.close()
                        break
                break
            else:
                log.close()
                break
        return  render_template("login.html", state = state)
    else:
        return  render_template("loginpage.html")

@app.route('/login', methods=['POST'])
def logingo():
    m_id = request.form['m_id']
    m_pw = request.form['m_pw']
    cur = mysql.connect().cursor()
    cur.execute("SELECT * FROM straw_user")
    datas = cur.fetchall()
    if(datas[0][0] == m_id and datas[0][1] == m_pw):
        session['logged_in'] = True
        cur.close()
        while(True):
            state = "use able"
            log = open("log.txt", "r")
            mtemp = log.readline()
            time.sleep(0.5)
            if os.path.exists("b" + str(mtemp) + ".txt"):
                log1 = open("b" + str(mtemp) + ".txt" , "r")
                for i in log1.readlines():
                    if(i == "0\r\n"):
                        state = "no"
                        log.close()
                        sef = open("log.txt", "w")
                        mtemp = int(mtemp) + 1
                        sef.writelines(str(mtemp))
                        sef.close()
                        break
                break
            else:
                log.close()
                break
        return render_template("login.html", state=state)
    cur.close()
    return  render_template("loginpage.html")

@app.route('/logoutpage')
def logoutpage():
    if(session['logged_in'] == True):
        session['logged_in'] = False
        return  render_template("loginpage.html")
    else:
        return  render_template("index.html")



if __name__ == '__main__':
    app.run()