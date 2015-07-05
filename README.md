# ScholarProxy
Google Scholar Proxy based on HttpClient   

谷歌学术搜索并不提供API，简单使用Http请求很容易跳转到人工验证页面，尤其是采用新的验证方式让代理或者调用谷歌学术的搜索结果越来越麻烦。   

SCholarProxy在同一个Ip地址下，利用Cookie模拟多台客户端向服务器请求数据，减少服务器判断为机器提交数据的概率。   

Web基于Aps.net的HttpModule,截获网络请求，然后分析请求类型，转发Html请求，图片类型请求转化为本地请求。对于Html请求可以实现对代码的修改。   

效果正在测试……


