#include <stdio.h>
void draw_basic_square(void);
int main(void)
{
	draw_basic_square();
	return 0;
}
void draw_basic_square(void)
{
	unsigned char a=0xa6, b[7], i;
	for(i=1;i<7;i++)//b[7]과 i<7의 7을 조정하여 크기조절 
		b[i]=0xa0+i;
		printf("%c%c", a, b[3]);
		printf("%c%c", a, b[4]);
		printf("\n");
		printf("%c%c", a, b[6]);
		printf("%c%c", a, b[5]);
		printf("\n");
}

