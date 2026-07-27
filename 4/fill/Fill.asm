// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, 
// the screen should be cleared.
@KBD
D=A
@temp
M=D
@SCREEN
D=A
@temp2
M=D

@temp2
D=M
@temp
D=M-D

@n
M=D // n = KBD-SCREEN

@UPDATE_SCREEN
0;JMP
(UPDATE_SCREEN)
    @n
    D=M
    @i
    D=M-D
    @END
    D;JEQ // jump if i == n

    @SCREEN
    D=A
    @i
    A=D+M // get the address of the current register
    M=-1

    @i
    M=M+1 // i+=1

    @UPDATE_SCREEN
    0;JMP
(END)
    @END
    0;JMP