var cnt = 0, dicenum = 1, dicehalf = 1, score = 0, lastdice = 0;
var p1score = 0, p2score = 0, p1count = 0, p2count = 0;
var dice = 0, dice2 = 0;
// 출처 2번 사운드추가
const audioContainer = document.querySelector('#audioContainer');
const playBtn = document.querySelector('.Throw');
const stopBtn = document.querySelector('.Stop');

function throwdice(){
    document.getElementById("Throw").src = "media/dice.wav";
	cnt = 1;
	dicenum ++;
	dicehalf = dicenum%2 + 1;
    //출처 3번 주사위굴리기
	const myIn = setInterval( function (){
                dice = (Math.floor(Math.random() * 6) + 1);
                dice2 = (Math.floor(Math.random() * 6) + 1);
				if(cnt == 1){
				document.getElementById("dice"+dicehalf).src = "media/dice"+dice+".png";
                document.getElementById("dice"+(dicehalf+2)).src = "media/dice"+dice2+".png";
                lastdice = dicehalf+"번 포인트 : " + (dice+dice2) + "점";
				score = (dice+dice2)
            }
				else if (cnt == 2){
					clearInterval(myIn);
				}
			},50);
			

}
function stop() {
	cnt = 2;
    if(dicehalf == 1){
	document.getElementById("pv1").value=lastdice;
	p1score = score;
	}
    else if(dicehalf == 2){
    document.getElementById("pv2").value=lastdice;
	p2score = score;

    }
	
	if(p1score !=0 && p2score !=0)
	{ result();}
}

function result(){
	if(p1score > p2score){
	document.getElementById("winner").value="Player 1 Win";
    p1count++;
    document.getElementById("ps1").textContent=p1count +"승";
    }
	if(p1score < p2score){
	document.getElementById("winner").value="Player 2 Win";
    p2count++;
    document.getElementById("ps2").textContent=p2count +"승"
    }
	else if(p1score == p2score)
	document.getElementById("winner").value="Draw";

    p1score = 0;
    p2score = 0;

}

function retry(){

	document.getElementById("pv1").value="   ";
	document.getElementById("pv2").value="   ";
	document.getElementById("winner").value="   ";
    cnt = 0, dicenum = 1, dicehalf = 1, score = 0, lastdice = 0;
    p1score = 0, p2score = 0, p1count = 0, p2count = 0;

    document.getElementById("ps1").textContent="0승";
    document.getElementById("ps2").textContent="0승";
    document.querySelector('.winner').value="New Game";
}
//출처 2번 사운드추가
function playAudio() {
    audioContainer.volume = 0.7;
    audioContainer.loop = true;
    audioContainer.play();
    
  }
  function stopAudio() {
    audioContainer.pause();  
  }
  function loadAudio() {
    const source = document.querySelector('#audioSource');
    source.src = `media/dice.wav`;
    audioContainer.load();
    playAudio();
  }
  playBtn.addEventListener('click', loadAudio);
  stopBtn.addEventListener('click', stopAudio)