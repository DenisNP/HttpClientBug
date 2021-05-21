# HttpClientBug

Windows 10 console output (response is ok, as it should be):
```
Request to https://www.nespresso.com/ru/ru/rest/V1/alice/customers/getProfile
OK got response, it works:
{"message":"The consumer isn't authorized to access %resources.","parameters":{"resources":"Nespresso_Alice::customer"}}
```

Ubuntu 18.04.4 LTS under Docker container output:
```
Request to https://www.nespresso.com/ru/ru/rest/V1/alice/customers/getProfile
2021-05-21T17:07:08.641137013Z app[web.1]: Error, it doesn't work
2021-05-21T17:07:08.660183142Z app[web.1]: System.Threading.Tasks.TaskCanceledException: The request was canceled due to the configured HttpClient.Timeout of 30 seconds elapsing.
2021-05-21T17:07:08.660211370Z app[web.1]:  ---> System.TimeoutException: The operation was canceled.
2021-05-21T17:07:08.665905984Z app[web.1]:  ---> System.Threading.Tasks.TaskCanceledException: The operation was canceled.
2021-05-21T17:07:08.665927532Z app[web.1]:  ---> System.IO.IOException: Unable to read data from the transport connection: Operation canceled.
2021-05-21T17:07:08.665931366Z app[web.1]:  ---> System.Net.Sockets.SocketException (125): Operation canceled
2021-05-21T17:07:08.665935070Z app[web.1]:    --- End of inner exception stack trace ---
2021-05-21T17:07:08.665938823Z app[web.1]:    at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.ThrowException(SocketError error, CancellationToken cancellationToken)
2021-05-21T17:07:08.665942184Z app[web.1]:    at System.Net.Sockets.Socket.AwaitableSocketAsyncEventArgs.GetResult(Int16 token)
2021-05-21T17:07:08.665949858Z app[web.1]:    at System.Net.Security.SslStream.ReadAsyncInternal[TIOAdapter](TIOAdapter adapter, Memory`1 buffer)
2021-05-21T17:07:08.665953888Z app[web.1]:    at System.Net.Http.HttpConnection.FillAsync(Boolean async)
2021-05-21T17:07:08.665957108Z app[web.1]:    at System.Net.Http.HttpConnection.ReadNextResponseHeaderLineAsync(Boolean async, Boolean foldedHeadersAllowed)
2021-05-21T17:07:08.665993234Z app[web.1]:    at System.Net.Http.HttpConnection.SendAsyncCore(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
2021-05-21T17:07:08.666006885Z app[web.1]:    --- End of inner exception stack trace ---
2021-05-21T17:07:08.666062764Z app[web.1]:    at System.Net.Http.HttpConnection.SendAsyncCore(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
2021-05-21T17:07:08.666068483Z app[web.1]:    at System.Net.Http.HttpConnectionPool.SendWithRetryAsync(HttpRequestMessage request, Boolean async, Boolean doRequestAuth, CancellationToken cancellationToken)
2021-05-21T17:07:08.666088000Z app[web.1]:    at System.Net.Http.RedirectHandler.SendAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
2021-05-21T17:07:08.666091993Z app[web.1]:    at System.Net.Http.DecompressionHandler.SendAsync(HttpRequestMessage request, Boolean async, CancellationToken cancellationToken)
2021-05-21T17:07:08.666168353Z app[web.1]:    at System.Net.Http.HttpClient.SendAsyncCore(HttpRequestMessage request, HttpCompletionOption completionOption, Boolean async, Boolean emitTelemetryStartStop, CancellationToken cancellationToken)
2021-05-21T17:07:08.666175588Z app[web.1]:    --- End of inner exception stack trace ---
2021-05-21T17:07:08.666178907Z app[web.1]:    --- End of inner exception stack trace ---
2021-05-21T17:07:08.666231490Z app[web.1]:    at System.Net.Http.HttpClient.SendAsyncCore(HttpRequestMessage request, HttpCompletionOption completionOption, Boolean async, Boolean emitTelemetryStartStop, CancellationToken cancellationToken)
2021-05-21T17:07:08.666237411Z app[web.1]:    at HttpClientBug.Program.DoRequest() in /app/HttpClientBug/Program.cs:line 40
```
