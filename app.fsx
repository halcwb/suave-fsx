#if BOOTSTRAP
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__
if not (System.IO.File.Exists "paket.exe") then let url = "https://github.com/fsprojects/Paket/releases/download/0.27.2/paket.exe" in use wc = new System.Net.WebClient() in let tmp = System.IO.Path.GetTempFileName() in wc.DownloadFile(url, tmp); System.IO.File.Move(tmp,System.IO.Path.GetFileName url);;
#r "paket.exe"
Paket.Dependencies.Install (System.IO.File.ReadAllText "paket.dependencies")
#endif
//---------------------------------------------------------------------
#I "packages/Suave/lib/net40"
#r "packages/Suave/lib/net40/Suave.dll"
open System
open Suave // always open suave
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful // for OK-result
open Suave.Web // for config
open Suave.Types

printfn "initializing script..."

let config =
    let port = System.Environment.GetEnvironmentVariable("PORT")
    { defaultConfig with
        logger = Logging.Loggers.saneDefaultsFor Logging.LogLevel.Verbose
        bindings = [ (if port = null then HttpBinding.mk' HTTP "127.0.0.1" 3000
                      else HttpBinding.mk' HTTP "0.0.0.0" (int32 port)) ] }

printfn "starting webserver ..."

let app = OK "Hello World!"

startWebServer config app
