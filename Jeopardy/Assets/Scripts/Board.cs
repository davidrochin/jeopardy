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

    string[,] categories = {
        { "Cultura general", "Anatomía I-II", "Bioquímica I-II", "Histología", "Embriología" },
        { "Cultura general", "Fisiología I-II", "Propedeutica I-II", "Microbiología", "Biología Molecular" },
        { "Cultura general", "Hematología", "Farmacología I-II", "Gastroenterología", "Infectología" },
        { "Cultura general", "Cardiología", "Urología", "Neurología", "Oncología" }
    };

    void Awake () {

        //Leer preguntas del CSV
        questionCollection = new QuestionCollection(Question.FromCSV());

        Build();
	}

    public void Populate(int level) {

        string[] levelCategories = GetLevelCategories(level);

        //Popular los encabezados
        for (int x = 0; x < cols; x++) {
            Cell cell = cells[x, 0].GetComponent<Cell>();
            cell.type = Cell.Type.Header;
            cell.textMesh.text = levelCategories[x];
            //cell.textMesh.font = Resources.Load("Fonts & Materials/LiberationSans SDF") as TMP_FontAsset;
            cell.textMesh.font = Resources.Load("Fonts & Materials/OpenSans-Bold SDF") as TMP_FontAsset;
            cell.textMesh.color = Color.white;
            cell.textMesh.fontSizeMax = 4f;
            //cell.textMesh.enableAutoSizing = false;
            //cell.textMesh.fontSize = 4f;
            //cell.textMesh.fontStyle = FontStyles.Bold;
        }

        //Popular las celdas de preguntas
        for (int x = 0; x < cols; x++) {
            for (int y = 1; y < rows; y++) {
                Question[] questions = questionCollection.GetAllWith(y * 200, level, x + 1);
                cells[x, y].GetComponent<Cell>().SetQuestions(questions);
                //Debug.Log(question);
            }
        }
    }

    public void PopulateRemaining(int level) {

        string[] levelCategories = GetLevelCategories(level);

        //Cambiar los encabezados
        for (int x = 0; x < cols; x++) {
            Cell cell = cells[x, 0].GetComponent<Cell>();
            cell.textMesh.text = levelCategories[x];
        }

        //Repopular las celdas que quedan de preguntas
        for (int x = 0; x < cols; x++) {
            for (int y = 1; y < rows; y++) {

                //Si a la celda le quedan preguntas activas
                if(cells[x, y].GetComponent<Cell>().HasActiveQuestions()) {
                    Cell cell = cells[x, y].GetComponent<Cell>();
                    //Question[] questions = questionCollection.GetAllWith(y * 200, level, x + 1);
                    Question[] questions = questionCollection.GetNumberWith(y * 200, level, x + 1, cell.GetActiveQuestions().Length);
                    cell.SetQuestions(questions);
                }

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
        Vector2 cellSize = new Vector2(boardSize.x / cols, boardSize.y / rows);
        Vector2 firstPos = transform.position + Vector3.up * (boardSize.y / 2f) + Vector3.left * (boardSize.x / 2f) + Vector3.down * (cellSize.y / 2f) + Vector3.right * (cellSize.x / 2f);
        cells = new GameObject[cols, rows];

        for (int x = 0; x < cols; x++) {
            for (int y = 0; y < rows; y++) {
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

    string[] GetLevelCategories(int level) {
        List<string> results = new List<string>();
        for (int c = 0; c < categories.GetLength(1); c++) {
            results.Add(categories[level - 1, c]);
        }
        return results.ToArray();
    }

    private void OnDrawGizmos() {

        //Dibujar el area que va a abarcar el tablero
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, boardSize);

        //Dibujar lineas para ver como quedaran las celdas
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
