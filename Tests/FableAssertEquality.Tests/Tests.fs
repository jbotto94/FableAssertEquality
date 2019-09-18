module Tests

open Testing


let AllTests =  testList "AllTests" []

open Fable.Core
open Fable.Core.JsInterop

flattenTest AllTests
