// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open visualizer
type rndStream()=
    let rnd = new System.Random()
    interface visualizer.Stream<int> with  
        member t.next() = rnd.Next(0,5000)

type maxPosInt()=
    let mutable curMax = 0
    let mutable count = 0
    override t.ToString() = "Maximum positive integer"
    interface visualizer.SAlgo<int> with
        member t.put(a) =if a>curMax then curMax <- a
                         count <- count + 1
        member t.result = curMax 
        member t.count = count
    
[<EntryPoint>]
let main argv = 
    let drawme = new visualizer.Drawer("Rand",(new rndStream()), (new maxPosInt()))
    drawme.run
    0 // return an integer exit code
