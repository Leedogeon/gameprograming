#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
typedef struct
{
        int order;
        int number;
        char shape[3];
}trump;
void make_card(trump m_card[]){
	int i,j;
	char shape[4][3] = { "♠", "◆", "♥", "♣" };
	for (i = 0; i < 4; i++){
		for (j = i * 13; j < i * 13 + 13; j++){
			m_card[j].order = i;
			strcpy(m_card[j].shape, shape[i]);
			int k = j % 13 + 1;
			if (k == 1)
			m_card[j].number = 'A';
				else if (k == 11)
					m_card[j].number = 'J';
				else if (k == 12)
					m_card[j].number = 'Q';
				else if (k == 13)
					m_card[j].number = 'K';
				else
					m_card[j].number = k;
        }
    }
}
void shuffle_card(trump m_card[]){
	int i,rnd;
	trump temp;
	srand((unsigned)time(NULL));
	for (i = 0; i < 52; i++){
		rnd = rand() % 52;
		temp = m_card[rnd];
		m_card[rnd] = m_card[i];
		m_card[i] = temp;;
    }
}
void display_card(trump m_card[]){
	int i;
	for (i = 0; i < 5; i++){
    	printf("%s", m_card[i].shape);
    	if (10 < m_card[i].number)
    		printf("%-2c\n", m_card[i].number);
    	else
    		printf("%-2d\n", m_card[i].number);
    }
}
int main(void)
{		
	int i,j,k;
	trump card[52], show[5];
	make_card(card);
	shuffle_card(card);
	for (i = 0; i < 5; i++)
		show[i] = card[i]; //임의로 카드 5장 뽑기
		for (i = 0; i < 4; i++){ //모양부터 순서대로
			for (j = 0; j < 4 - i; j++){
				if (show[j].order > show[j + 1].order){
					trump temp = show[j + 1];
					show[j + 1] = show[j];
					show[j] = temp;
				}
			}
		}
	for (i = 0; i < 4; i++){ //숫자 정렬
		for (j = 0; j < 4 - i; j++){
			if ((show[j + 1].order == show[j].order) && (show[j + 1].number < show[j].number)){
				trump temp = show[j + 1];
				show[j + 1] = show[j];
				show[j] = temp;
			}
		}
	}
	for (i = 0; i < 4; i++){ //모양은 다르지만 같은 숫자인 경우
		for (j = 1; j < 5; j++)
			{
				if (show[i].number == show[j].number){ //숫자가 같으면
					for (k = j; k > i + 1; k--){ //붙인다
						trump temp = show[k];
						show[k] = show[k - 1];
						show[k - 1] = temp;
			}
			}
		}
	}
	display_card(show);
	printf("\n");
	return 0;
}
