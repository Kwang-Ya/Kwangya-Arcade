using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }

    public TetrominoData[] tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int spawnPosition;

    public Tile grayTile;

    // UI
    public bool isGameover = false;
    public Text scoreText;
    public Text timeText;
    public Text gameclearText;
    public GameObject gameoverUI;
    public GameObject gameclearUI;
   
    private int score = 0;
    private float playTime = 0f;
    int brickMakeCount = 0;
    private const int spawnTimeDelay = 5;
    private const int spawnTime = 45;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    private void Update()
    {
        playTime += Time.deltaTime;
        timeText.text = "Time : " + Mathf.Ceil(playTime).ToString();


        if (playTime >= brickMakeCount * spawnTimeDelay + spawnTime)
        {
            brickMakeCount++;
            SpawnBrick();
        }

        if (playTime > 200)
            activePiece.stepDelay = 0.5f;
        else if (playTime > 100)
            activePiece.stepDelay = 0.7f;
    }

    public void SpawnBrick()
    {
        Clear(activePiece);
        if (!IsValidPosition(activePiece,activePiece.position+new Vector3Int(0,-1,0)))
            activePiece.Lock();
        Set(activePiece);

        for(int i=8; i>=-10;i--)
        {
            for(int j=-5;j<boardSize.x-5;j++)
            {
                Vector3Int pos = new Vector3Int(j, i, 0);
                bool check = false;
                for (int p = 0; p < activePiece.cells.Length; p++)
                {
                    Vector3Int tilePosition = activePiece.cells[p] + activePiece.position;
                    if(pos == tilePosition)
                    {
                        check = true;
                        break;
                    }
                }

                if (this.tilemap.HasTile(pos) && !check)
                {
                    this.tilemap.SetTile(pos+new Vector3Int(0,1,0), this.tilemap.GetTile(pos));
                }
                else
                {
                    this.tilemap.SetTile(pos + new Vector3Int(0, 1, 0), null);
                }
            }
        }

        for(int i=-5;i<boardSize.x-5;i++)
        {
            this.tilemap.SetTile(new Vector3Int(i,-10,0), grayTile);
        }
    }

    public void SpawnPiece()
    {
        Debug.Log(1);

        int random = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else
        {
            Debug.Log("!!");
            GameOver();
        }

        Set(this.activePiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;
        int lineCount = 0;      // 한 번에 사라진 행 개수 확인

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
                lineCount++;

                //Debug.Log(lineCount);

                if (lineCount > 1)
                {
                    AddScore(lineCount * 200);
                    //Debug.Log("score : " + lineCount * 200);
                    lineCount = 0;
                }
                else if (lineCount == 1)
                    AddScore(100);
            }
            else
            {
                row++;
            }
        }
    }

    private void AddScore(int num)
    {
        score += num;

        if (score >= 1000)      // 게임 클리어 4000으로 고치기
            GameClear();

        scoreText.text = "Score : " + score;

    }

    private void GameClear()
    {
        this.tilemap.ClearAllTiles();
        Time.timeScale = 0;

        gameclearText.text = "Clear Time : " + Mathf.Ceil(playTime).ToString() + "\nScore : " + score;
        gameclearUI.SetActive(true);
    }

    private void GameOver()
    {
        this.tilemap.ClearAllTiles();
        Time.timeScale = 0;

        gameoverUI.SetActive(true);
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position) || this.tilemap.GetTile(position) == grayTile)
            {
                return false;
            }
        }

        return true;
    }

    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }

            row++;
        }
    }
}
