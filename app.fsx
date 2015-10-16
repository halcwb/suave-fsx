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
