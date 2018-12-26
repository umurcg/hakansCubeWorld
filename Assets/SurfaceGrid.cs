using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceGrid : MonoBehaviour
{

    public int numberOfCell = 100;
    public GameObject gridTestExample;

    float unitCellSize;

    bool[,] filledCells;
    Renderer rend;
    

    // Start is called before the first frame update
    void Awake()
    {
        rend=GetComponent<Renderer>();

        var surfaceEdgeSize = rend.bounds.max.x - rend.bounds.min.x;
        unitCellSize = surfaceEdgeSize / numberOfCell;

        filledCells = new bool[numberOfCell,numberOfCell];
        
    }

    private void Start()
    {
      //Test
      for(int i = 0; i < 10; i++)
        {
            //print(findPositionForObjectGroup(3, 2));
        }
       
    }

    Vector3 findPositionForObjectGroup(int numberOfRow,int numberOfColumn)
    {
        int leftTry= 100;
        while (leftTry > 0)
        {
            leftTry -= 1;

            //First find random cell in grid
            var randomRow = Random.Range(0, numberOfCell-numberOfRow);
            var randomColumn = Random.Range(0, numberOfCell-numberOfColumn);
            

            //Check weather or not all cells are avaible
            for(int r = 0; r < numberOfRow;r++)
            {
                for(int c = 0; c < numberOfColumn; c++)
                {
                    if (filledCells[randomRow + r, randomColumn + c])
                        continue;
                }
            }

            var startPosition = rend.bounds.min;

            //If code reaches it means cells are avaible
            //Find middle position of cells and fill cells
            Vector3 positionSum = Vector3.zero;

            for (int r = 0; r < numberOfRow; r++)
            {
                for (int c = 0; c < numberOfColumn; c++)
                {
                    var row = randomRow + r;
                    var column = randomColumn + c;
                    filledCells[row,column] = true;
                    positionSum += startPosition + new Vector3(row * unitCellSize + unitCellSize / 2, column * unitCellSize + unitCellSize / 2, 0);
                        
                }
            }

            positionSum /= (numberOfRow * numberOfColumn);
            return positionSum;


           
        }

        return Vector3.zero ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void getGridSize()
    {

    }

}
