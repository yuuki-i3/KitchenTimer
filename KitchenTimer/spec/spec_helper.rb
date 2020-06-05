require "rubygems"
require "appium_lib"


android_caps = {
  caps: {
    "platformName": "Android",
    "disableWindowAnimation": "true",
    "deviceName": "emulator-5554",
    "automationName": "uiautomator2",
    "platformVersion": "9.0",
    "app": "/Users/kondouyuuhime/Projects/KitchenTimer/KitchenTimer/bin/Debug/com.tutorial.kitchentimer.apk", 
    #"appPackage": "com.tutorial.kitchentimer"
  },
  appium_lib: {
    wait: 10
  }
}

RSpec.configure { |c|
  c.before(:each) {
    @driver = Appium::Driver.new(caps, true)
    @driver.start_driver
    Appium.promote_appium_methods Object
  }

  c.after(:each) {
    @driver.driver_quit
  }
}
