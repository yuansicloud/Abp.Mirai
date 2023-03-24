# Abp.Mirai


针对 ABP vNext 框架，封装的mirai-api-http模块，包含对http、websocket、reverse websocket、webhook的4种协议的支持

> 推荐结合Mirai团队的[mirai-api-http](https://github.com/project-mirai/mirai-api-http)使用

## 一、简要介绍

**Abp.Mirai** 库是针对于mirai-api-http进行了二次封装的模块，与 ABP vNext 框架深度集成。开发人员如果是基于 ABP vNext  框架开发项目，集成本模块以后，可以快速实现同mirai-api-http的对接

本库主要参考了[Mirai.Net](https://github.com/project-mirai/mirai-api-http)进行基础数据结构和部分功能的实现。Mirai.Net主要适合开发Console类型项目，Abp.Mirai在Mirai.Net的基础上，和ABP vNext框架进行了结合，使之更方便使用，对较大的服务器项目进行了支持。

[Mirai](https://github.com/project-mirai/mirai-api-http) 是一个在全平台下运行，提供 QQ Android 协议支持的高效率机器人库

## 二、协议API 支持情况

| 功能             | 支持情况                                                     | 文档                                   |
| ---------------- | ------------------------------------------------------------ | -------------------------------------- |
| http     | ![Support](https://img.shields.io/badge/-部分支持-orange.svg) | [访问文档](/docs/http.md)         |                                        |
| ws | ![Support](https://img.shields.io/badge/-不支持-red.svg)     |                                        |
| reverse-ws | ![Support](https://img.shields.io/badge/-不支持-red.svg)     |                                        |
| webhook   | ![Support](https://img.shields.io/badge/-部分支持-orange.svg) | [访问文档](/docs/webhook.md) |