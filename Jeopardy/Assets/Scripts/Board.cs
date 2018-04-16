using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Board : MonoBehaviour {

    [Header("Board")]
    public Vector2 boardSize;
    public Color backgroundColor;

    [Header("Cells")]
    public int rows = 6; public int cols = 6;
    public Color cellColor;
    public float cellMargin;

    public GameObject background;
    public GameObject[,] cells;
    public QuestionCollection questionCollection;

	void Awake () {
        Build();
	}

    public void Populate(Question[] questions, string[] categories) {

        //Iniciar la coleccion de preguntas
        questionCollection = new QuestionCollection(questions);

        //Popular los encabezados
        for (int x = 0; x < cols; x++) {
            Cell cell = cells[x, 0].GetComponent<Cell>();
            cell.type = Cell.Type.Header;
            cell.textMesh.text = categories[x];
            //cell.textMesh.font = Resources.Load("Fonts & Materials/LiberationSans SDF") as TMP_FontAsset;
            cell.textMesh.font = Resources.Load("Fonts & Materials/Impact SDF") as TMP_FontAsset;
            cell.textMesh.color = Color.white;
            cell.textMesh.fontSizeMax = 4f;
            //cell.textMesh.enableAutoSizing = false;
            //cell.textMesh.fontSize = 4f;
            //cell.textMesh.fontStyle = FontStyles.Bold;
        }

        //Popular las celdas de preguntas
        for (int x = 0; x < cols; x++) {
            for (int y = 1; y < rows; y++) {
                Question question = questionCollection.RandomWithValueAndCategory(y * 200, (Question.Category) x);
                cells[x, y].GetComponent<Cell>().SetQuestion(question);
                Debug.Log(question);
            }
        }
    }

    public void Build() {

        //Crear el fondo
        background = new GameObject("Background");
        background.transform.position = transform.position;
        background.transform.parent = transform;
        SpriteRenderer backgroundSpriteRenderer = background.AddComponent<SpriteRenderer>();
        backgroundSpriteRenderer.sprite = Resources.Load<Sprite>("default");
        background.transform.localScale = boardSize;
        backgroundSpriteRenderer.color = backgroundColor;
        backgroundSpriteRenderer.sortingOrder = -2;

        //Crear las celdas
        Vector2 cellSize = new Vector2(boardSize.x / 6f, boardSize.y / 6f);
        Vector2 firstPos = transform.position + Vector3.up * (boardSize.y / 2f) + Vector3.left * (boardSize.x / 2f) + Vector3.down * (cellSize.y / 2f) + Vector3.right * (cellSize.x / 2f);
        cells = new GameObject[cols, rows];

        for (int x = 0; x < 6; x++) {
            for (int y = 0; y < 6; y++) {
                GameObject cell = new GameObject("Celda " + (x + 1) + " - " + (y + 1));
                cell.transform.position = firstPos + Vector2.right * cellSize.x * x + Vector2.down * cellSize.y * y;
                cell.transform.parent = transform;
                SpriteRenderer cellSpriteRenderer = cell.AddComponent<SpriteRenderer>();
                cellSpriteRenderer.sprite = Resources.Load<Sprite>("default");
                cellSpriteRenderer.color = cellColor;
                cellSpriteRenderer.sortingOrder = -1;
                cell.transform.localScale = new Vector3(cellSize.x - cellMargin * 2f, cellSize.y - cellMargin * 2f, 1f);

                //Si es un encabezado, hacerla un poco menos alta
                if (y == 0) { cell.transform.localScale = new Vector3(cell.transform.localScale.x, cell.transform.localScale.y - cellMargin * 2f, 1f); } 

                Cell cellComponent = cell.AddComponent<Cell>();
                if(y == 0) { cellComponent.type = Cell.Type.Header; }

                //Registrar la celda en el array de celdas
                cells[x, y] = cell;
            }
        }
    }

    private void OnDrawGizmos() {

        //Dibujar el area que va a abarcar el tablero
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, boardSize);

        //Dibujar lineas para ver como quedaran las celdas
        int cols = 6; int rows = 6;
        float colWidth = boardSize.x / cols;
        float rowHeight = boardSize.y / rows;

        Vector2 topLeft = (Vector2)transform.position + Vector2.up * (boardSize.y / 2) + Vector2.left * (boardSize.x / 2);
        Vector2 bottomLeft = (Vector2)transform.position + Vector2.down * (boardSize.y / 2) + Vector2.left * (boardSize.x / 2);
        Vector2 topRight = (Vector2)transform.position + Vector2.up * (boardSize.y / 2) + Vector2.right * (boardSize.x / 2);

        //Dibujar las columnas
        for (int x = 1; x < cols; x++) {
            Gizmos.DrawLine(topLeft + Vector2.right * colWidth * x, bottomLeft + Vector2.right * colWidth * x);
        }

        //Dibujar los renglones
        for (int y = 1; y < rows; y++) {
            Gizmos.DrawLine(topLeft + Vector2.down * rowHeight * y, topRight + Vector2.down * rowHeight * y);
        }

    }

}
