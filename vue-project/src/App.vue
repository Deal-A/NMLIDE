<script setup lang="ts">
import { reactive, ref, watch } from "vue"

import { Nodes, Edges, Layouts } from "v-network-graph"

import * as vNG from "v-network-graph"
import {
  ForceLayout,
  ForceNodeDatum,
  ForceEdgeDatum,
} from "v-network-graph/lib/force-layout"

let nodeCount = ref(20)
const nodes = reactive({})
const edges = reactive({})
const layouts = reactive({})

const mpConfig = 
{
  inputs:2,
  outputs:1,
  hidenLayersArr : [10,3,7]
}

const gridStepG = 50


// initialize network
buildNetwork(nodeCount.value, nodes, edges)

watch(nodeCount, () => {
  console.log("ааа");
  buildNetwork(nodeCount.value, nodes, edges)
})


const configs = reactive(
  vNG.defineConfigs({
    view: {
      layoutHandler: new vNG.GridLayout({ grid: 10 })
      // layoutHandler: new ForceLayout({
      //   positionFixedByDrag: false,
      //   positionFixedByClickWithAltKey: true,
      //   createSimulation: (d3, nodes, edges) => {
      //     // d3-force parameters
      //     const forceLink = d3.forceLink<ForceNodeDatum, ForceEdgeDatum>(edges).id(d => d.id)
      //     return d3
      //       .forceSimulation(nodes)
      //       .force("edge", forceLink.distance(40).strength(0.5))
      //       .force("charge", d3.forceManyBody().strength(-800))
      //       .force("center", d3.forceCenter().strength(0.15))
      //       .alphaMin(0.001)

      //   //     // * The following are the default parameters for the simulation.
      //   //     // const forceLink = d3.forceLink<ForceNodeDatum, ForceEdgeDatum>(edges).id(d => d.id)
      //   //     // return d3
      //   //     //   .forceSimulation(nodes)
      //   //     //   .force("edge", forceLink.distance(100))
      //   //     //   .force("charge", d3.forceManyBody())
      //   //     //   .force("collide", d3.forceCollide(50).strength(0.2))
      //   //     //   .force("center", d3.forceCenter().strength(0.05))
      //   //     //   .alphaMin(0.001)
      //   // }
      // }),
    },
    node: {
      label: {
        visible: false,
      },
    },
  })
)


function getArrByN(n:number,index:number):number[]

{
    //let index = i
    let resArr: number[] = []
    
    for (let i = 0;i<n;i++)
    {
        resArr[i] = index++;
    }

    return resArr
}

function getNodeIndexArr(ni:number,no:number,arrNH:number[]):number[][]
{
    //const resArr = [...Array(arrNH.length+2)].map((_, i) => i)

    let resArr:number[][] = []
    let index:number = 0

    let n:number = arrNH.length + 2
    
    for (let i = 0;i<n;i++)
    {
        if (0==i)
        {
            resArr[i] = getArrByN(ni,index)
            index+=ni
        }
        else if (i==(n-1))
        {
            resArr[i] = getArrByN(no,index)
            index+=no
        }
        else
        {
            // из скрытого слоя
            let hnc = arrNH[i-1]
            resArr[i] = getArrByN(hnc,index)
            index+=hnc 
        }
    }

    return resArr

}

function createNeuronsRelations(nodeIndexArr:number[][])
{
    let layersN:number = nodeIndexArr.length

    let resEdgeMap:number[][] = []
    
    // по слоям
    for(let i=0;i<(layersN-1);i++)
    {
        // по нейронам в текущем слое
        for(let j = 0;j<nodeIndexArr[i].length;j++)
        {
            for(let k=0;k<nodeIndexArr[i+1].length;k++)
            {
                resEdgeMap.push([nodeIndexArr[i][j],nodeIndexArr[i+1][k]])
            }
        }
    }

    return resEdgeMap
}

function getPixXYCoord(i:number,j:number,bX:number,bY:number,gridStep:number)
{
  return [i*gridStep,j*gridStep]
}

function getLayoutCoordinates(nodeIndexArr:number[][],gridStep:number)
{
  // пары координат xy
  let resXYArr:number[][] = []

  const gridXCells = nodeIndexArr.length
  const gridYCells = Math.max( ...nodeIndexArr.map((arr)=>arr.length))
  // пустой массив сетки
  let grid:number[][] = [...Array(gridYCells)].map((arr)=>[...Array(gridXCells)])

  const getDelta = (curArrLen:number,gridYSize:number) =>
  {
    const vacancyCellsCount = gridYSize-curArrLen
    return Math.floor( vacancyCellsCount/2)
  }
  
  for(let i = 0;i<gridXCells;i++)
  {
    for(let j = 0;j<gridYCells;j++)
    {
        if (typeof nodeIndexArr[i][j] === 'undefined') {
            break;
        } 
        
        const delta = getDelta(nodeIndexArr[i].length,gridYCells)

        grid[j+delta][i] = nodeIndexArr[i][j]
        resXYArr.push(getPixXYCoord(i,j+delta,0,0,gridStep))
    }
  }

  return resXYArr
}

function buildNetwork(count: number, nodes: vNG.Nodes, edges: vNG.Edges) {
  
  
  const nodeIndexArr = getNodeIndexArr(mpConfig.inputs,mpConfig.outputs,[...mpConfig.hidenLayersArr])
  
  const layoutCoordinates = getLayoutCoordinates(nodeIndexArr,gridStepG)

  console.log('Координаты')
  console.log(layoutCoordinates)

  const edgesPairs = createNeuronsRelations(nodeIndexArr)

  const layers = nodeIndexArr.length
  const neuronsLastLayer = nodeIndexArr[layers-1].length

  // // крайний индекс нейрона +1
  const nCount = nodeIndexArr[layers-1][neuronsLastLayer-1] + 1

  const idNumsMP = [...Array(nCount)].map((_, i) => i)

  console.log(idNumsMP)

  const idNums = [...Array(count)].map((_, i) => i)

  // nodes
  const newNodes = Object.fromEntries(idNumsMP.map(id => [`node${id}`, {}]))

  Object.keys(nodes).forEach(id => delete nodes[id])
  Object.assign(nodes, newNodes)


  // edges
  const makeEdgeEntry = (id1: number, id2: number) => {
    return [`edge${id1}-${id2}`, { source: `node${id1}`, target: `node${id2}` }]
  }

  const makeLayoutEntry = (id1: number, id2: number,index:number) => {
    return [`node${index}`, { x: id1, y: id2 }]
  }

  const newEdges = Object.fromEntries([
      ...edgesPairs
      .map(([n, m]) => makeEdgeEntry(n, m)),
  ])

  const newLayouts = 
  {
      nodes: Object.fromEntries([
        ...layoutCoordinates
        .map(([n, m],index) => makeLayoutEntry(n, m,index)),
      ])
  }

  console.log('Координаты новые')
  console.log(newLayouts)

  console.log('Ребра новые')
  console.log(newEdges)


  Object.keys(edges).forEach(id => delete edges[id])
  Object.assign(edges, newEdges)

  // const newLayouts1: Layouts = {
  //   nodes: {
  //     node0: { x: 500, y: 500 },
  //     node1: { x: 500, y: 550 },
  //     node2: { x: 500, y: 600 },
  //     node3: { x: 700, y: 450 },
  //     node4: { x: 700, y: 500 },
  //     node5: { x: 700, y: 550 },
  //     node6: { x: 700, y: 600 },
  //     node7: { x: 700, y: 650 },
  //   }
  // }

  // console.log('Координаты статика')
  // console.log(newLayouts1)
  // console.log(typeof( newLayouts1))

  Object.assign(layouts, newLayouts)

}

const zoomLevel = ref(1.5)

// ref="graph"
const graph = ref<vNG.Instance>()
// graph.zoomIn()
// graph.zoomIn()

// function ncu_func()
// {
//   console.log("ncu_func");
//   nodeCount+=1;
// }

</script>

<template>
  <div class="demo-control-panel">
    <!-- <label>Node count:</label> -->
    
    
    <!-- <input type="number" id="quantity" name="quantity" min="0" max="1000" step="1"> -->
    <!-- <el-input-number v-model="nodeCount" :min="3" :max="200" /> -->

    <!-- <button id="ncu" @click="ncu_func()">+</button>
    <button id="ncd" @click="ncd_func()">-</button> -->

    <!--<el-button @click="graph?.zoomIn()">Zoom In</el-button>-->
  
    <!-- <label>(&lt;= 200)</label> -->
  </div>

  <!-- <script>
    document.getElementById('ncu').onclick = ()=>{nodeCount+=1;};
    document.getElementById('ncd').onclick = ()=>{nodeCount-=1;};
  </script> -->

  <v-network-graph
    ref="graph"
    v-model:zoom-level="zoomLevel"
    :nodes="nodes"
    :edges="edges"
    :configs="configs"
    :layouts="layouts"
  />
</template>
