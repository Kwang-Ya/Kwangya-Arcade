using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public int CLEAR_SCORE = 3000;      // 클리어 조건

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
    public GameObject spawnUI;
    public GameObject lineClearUI;

    private int score = 0;
    private float playTime = 0f;
    int brickMakeCount = 0;
    private const int spawnTimeDelay = 13;
    private const int spawnTime = 45;

    AudioSource audioSource;
    public AudioClip audioMove;
    public AudioClip audioDrop;
    public AudioClip audioClear;

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
        this.audioSource = GetComponent<AudioSource>();

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
        if (!isGameover)
        {
            playTime += Time.deltaTime;
            timeText.text = "Time : " + Mathf.Ceil(playTime).ToString();

            if (playTime >= brickMakeCount * spawnTimeDelay + spawnTime)
            {
                brickMakeCount++;
                SpawnBrick();
                BrickSpawnButton();
            }

            if (playTime > 200)
                activePiece.stepDelay = 0.5f;
            else if (playTime > 100)
                activePiece.stepDelay = 0.7f;
        }
            
    }

    public void BrickSpawnButton()
    {
        spawnUI.gameObject.SetActive(true);
        Invoke("DeactiveBrickSpawnButton", 0.8f);
    }

    public void DeactiveBrickSpawnButton()
    {
        spawnUI.gameObject.SetActive(false);
    }

    public void SpawnBrick()
    {
        Clear(activePiece);
        if (!IsValidPosition(activePiece,activePiece.position+new Vector3Int(0,-1,0)))
            activePiece.Lock();
        Set(activePiece);

        for (int i=8; i>=-10;i--)
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
        int random = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else
        {
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

                if (lineCount > 1)
                {
                    AddScore(lineCount * 200);
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

        if (score >= CLEAR_SCORE)      // 게임 클리어 조건
            GameClear();

        scoreText.text = "Score : " + score;

    }

    private void GameClear()
    {
        this.tilemap.ClearAllTiles();

        gameclearText.text = "Clear Time : " + Mathf.Ceil(playTime).ToString() + "\nScore : " + score;
        gameclearUI.SetActive(true);

        ClearManager.stageClear[2] = true;
        Invoke("ChangeScene", 2f);
    }

    private void GameOver()
    {
        isGameover = true;
        this.tilemap.ClearAllTiles();

        gameoverUI.SetActive(true);
        Invoke("ChangeScene", 2f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("JumpingAction");
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

        Debug.Log("Line clear");
        LineClearButton();
    }

    public void LineClearButton()
    {
        PlaySound("Clear");
        lineClearUI.gameObject.SetActive(true);
        Invoke("DeactiveLineClearButton", 0.8f);
    }

    public void DeactiveLineClearButton()
    {
        lineClearUI.gameObject.SetActive(false);
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "Move":
                audioSource.clip = audioMove;
                break;
            case "Drop":
                audioSource.clip = audioDrop;
                break;
            case "Clear":
                audioSource.clip = audioClear;
                break;
        }

        audioSource.Play();
    }
}
