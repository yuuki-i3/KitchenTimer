#require "rubygems"
require "appium_lib"


  caps = {
    "platformName": "Android",
    "disableWindowAnimation": "true",
    "deviceName": "emulator-5554",
    "automationName": "uiautomator2",
    "platformVersion": "9.0",
    "app": "/Users/kondouyuuhime/Projects/KitchenTimer/KitchenTimer/bin/Debug/com.tutorial.kitchentimer-Signed.apk", 
    "appPackage": "com.tutorial.kitchentimer"
  }



    @driver = Appium::Driver.new(caps, true)
    @driver.start_driver
    Appium.promote_appium_methods Object

    el1 = driver.find_elements(:id, "com.tutorial.kitchentimer:id/Add10MinButton")
    el1.click
    el2 = driver.find_elements(:id, "com.tutorial.kitchentimer:id/Add10SecButton")
    el2.click
    el3 = driver.find_elements(:id, "com.tutorial.kitchentimer:id/StartButton")
    el3.click
    el4 = driver.find_elements(:id, "com.tutorial.kitchentimer:id/StartButton")
    el4.click
    el5 = driver.find_elements(:id, "com.tutorial.kitchentimer:id/ClearButton")
    el5.click
    


    @driver.driver_quit