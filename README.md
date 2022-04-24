# Anti.LockScreen
一个在域中使用的小工具，通过定时发送 Num Lock 键，阻止 Windows 自动锁定屏幕
仅在没有正常鼠标,键盘操作的情况下发送, 不影响正常工作

工作原理：
使用使用 Win32 API GetLastInputInfo 获取系统上次用户输入事件，如果超过一定时间（默认 5 分钟，可以设置）没有用户活动
就使用 keybd_event 发送 Num Lock 两次模拟用户输入来实现阻止 Windows 自动锁屏

特性：
   可以设置连续发送一定次数后（默认12次）不再发送，当有用户输入后重新计时
   当动手锁屏后自动停止发送
   启动后自动隐藏在任务栏

A gadget used in a domain that prevents Windows from automatically locking the screen by sending the <Num Lock> key
Send only when there is no normal mouse or keyboard operation, it will not affect normal work

working principle:
Use Win32 API GetLastInputInfo to get the last user input event of the system, if there is no user activity for more than a certain time (default 5 minutes, can be set)
Just use keybd_event to send Num Lock twice to simulate user input to prevent Windows from automatically locking the screen

characteristic:
    It can be set to send continuously for a certain number of times (default 12 times) and no longer send, and re-start when there is user input
    Automatically stop sending when you manually lock the screen
    Automatically hide in the taskbar after startup
