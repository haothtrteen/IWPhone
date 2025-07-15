# IWPhone Toolbox
iwphone 爱玩机PC端（Device Toolbox）

## About
IWPhone on Anywhere
>The original Android IWPhone Toolbox is a toolbox that helps people unlock and use Android functions more easily. We will now launch a toolbox that can run on the PC version and add more new functions.

iwphone （爱玩机PC端）是一款基于 WPF（C#） 开发的 安卓设备深度管理与玩机工具箱，旨在为安卓极客、普通用户及开发者提供一站式设备管理解决方案。通过可视化界面与模块化设计，集成设备信息查看、手机投屏、驱动安装、应用管理、插件扩展等核心功能，让安卓设备的“玩机”与“管理”更简单、高效。

无论是调试开发、日常使用，还是个性化改造，WPF玩机助手都能满足你的需求！

## Power
核心功能

1. 设备信息全景视图

- 
实时显示设备基础信息：型号、品牌、系统版本（Android/MIUI/ColorOS等）、内核版本、内存/存储状态。
- 
支持查看硬件参数（CPU/GPU/屏幕分辨率）、网络状态（Wi-Fi/移动数据）、电池健康度。
- 
设备截图/录屏：一键捕获屏幕画面或录制操作视频（保存至本地）。

2. 爱玩互联·手机投屏（基于scrcpy）

- 
电脑投手机：将安卓设备屏幕实时投射到电脑，支持触控反向控制（电脑鼠标/键盘操作手机）。
- 
手机投电脑：将手机画面作为电脑的副屏或扩展屏（需设备支持）。
- 
低延迟优化：采用高效的ADB协议传输，确保投屏流畅度（兼容主流安卓10+设备）。

3. 驱动与依赖管理

- 
自动检测驱动：识别设备所需的关键驱动（如ADB驱动、厂商定制驱动），一键安装/修复。
- 
依赖库管理：自动下载并安装玩机常用工具的依赖（如Fastboot、Magisk工具包），避免手动配置。
- 
驱动状态监控：实时提示驱动异常（如未安装、版本冲突），并提供修复建议。

4. 应用深度管理

- 
应用市场集成：支持APK直接安装，支持安装、卸载。
- 
应用备份/还原：一键备份应用数据（含APK+私有目录），支持从备份恢复。
- 
应用冻结/精简：禁用系统预装冗余应用（如厂商广告APP），释放存储空间。

5. 插件模块仓库

- 
开源插件生态：支持用户自定义插件（基于C#/WPF开发），扩展工具功能（如字体替换、主题美化、性能监控）。
- 
官方插件库：内置常用插件（如字体安装器、主题引擎、root权限管理【待开发】），一键安装启用。
- 
插件管理：支持插件启用/禁用、版本更新、依赖检查，确保稳定性。

6. 设备激活与安全工具

- 
设备解锁助手：支持主流厂商（小米/华为/OPPO等）的Bootloader解锁指导与工具集成。
- 
Recovery刷入：一键下载并刷入第三方Recovery（如TWRP），支持备份原厂Recovery。
- 
安全检测：扫描设备是否存在恶意软件、系统漏洞，提供修复建议。

技术架构

- 
前端：WPF（Windows Presentation Foundation）—— 提供现代化UI界面，支持高DPI适配与流畅交互。
- 
后端：C#语言开发，基于ADB协议与安卓设备通信（通过
"Android Debug Bridge"）。
- 
扩展机制：插件系统基于C#的MEF（Managed Extensibility Framework），支持动态加载/卸载模块。
- 
跨平台潜力：核心逻辑可扩展至macOS/Linux（需适配ADB环境与UI框架）。

项目特色

- 
零门槛玩机：可视化操作替代命令行，普通用户也能轻松完成高级操作（如刷Recovery、安装Magisk模块）。
- 
模块化设计：功能通过独立插件实现，用户可自由选择启用/禁用，避免冗余功能干扰。
- 
社区驱动生态：官方维护核心功能，用户/开发者可贡献插件（如游戏优化工具、办公效率插件），丰富工具场景。
- 
稳定与安全：所有操作均经过严格测试，关键功能（如驱动安装、Recovery刷入）提供“回滚”选项，降低变砖风险。

## Supported
Android 5.1 ~ 14 (IWPhone on Android)  
Windows 10~11 (IWPhone on Windows)
>We will try our best to adapt to more versions in the future versions.

## Install
- IWPhone on Android ：https://www.coolapk.com/apk/com.byyoung.setting
- IWPhone on Windows ：[@Telegram channel](https://t.me/haothirteen)

## For Developers
贡献与社区

- 
提交Issue：在使用中遇到问题或功能需求，可通过GitHub Issues反馈。
- 
开发插件：参考"iwphone插件框架文档" (./iwphone_ModRunner-1.0.0/iwphone.md)，基于iwphone框架开发自定义功能。
- 
代码贡献：欢迎提交PR优化核心功能（如驱动安装逻辑、UI交互、模块插件扩展）。


## All
总结

iwphone 爱玩机PC端是一款“为玩机而生”的安卓管理工具，通过可视化界面与模块化设计，降低了PC-安卓深度操作的门槛，同时保留了强大的扩展性。无论是普通用户日常使用，还是极客玩家探索系统潜力，它都是你的“全能玩机助手”！

📥 下载地址："GitHub Releases" (https://github.com/haothtrteen/IWPhone/tree/main)

💬 交流群："QQ群/Telegram链接" (http://qm.qq.com/cgi-bin/qm/qr?_wv=1027&k=1S5AtdA0Q6KVJlr3anr9MBE-_qg6ds6w&authKey=XoyYccfL5S4b2dKkP7PGxtKJXYIcrm%2Bj2F3ajHWo5AQ4CsvgmIQX82tXDshKz0P9&noverify=0&group_code=792885692)（待补充）

注：部分功能需设备解锁Bootloader或Root权限，请谨慎操作！