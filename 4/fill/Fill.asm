// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, 
// the screen should be cleared.

@KEYCHECK
0;JMP

(BLACKEN)
    // we want to set M=1 for every pixel in SCREEN (131072/16 = 8192 total)
    // the last register is KEYBOARD-1
    
    // initialization
    @i
    M=0

    @SCREEN
    D=A
    
    @n
    M=D // n is the current number register we are on

    (BLACKEN_LOOP)
        @KEYBOARD
        D=M
        @n
        D=M-D
        @KEYCHECK
        D;JEQ // jump if n = KEYBOARD

        @n
        M=-1

        @i
        M=M+1

        @i
        D=M

        @n
        M=D

        @BLACKEN_LOOP
        0;JMP

(CLEAR)
    // initialization
    @k
    M=0

    @SCREEN
    D=A
    
    @o
    M=D // n is the current number register we are on

    (CLEAR_LOOP)
        @KEYBOARD
        D=M
        @o
        D=M-D
        @KEYCHECK
        D;JEQ // jump if n = KEYBOARD

        @o
        M=0

        @k
        M=M+1

        @k
        D=M

        @o
        M=D

        @CLEAR_LOOP
        0;JMP
(KEYCHECK)
    // check if keyboard is emitting enything
    @KEYBOARD
    D=M
    @BLACKEN
    D;JNE
    
    @CLEAR
    0;JMP

    @KEYCHECK
    0;JMP