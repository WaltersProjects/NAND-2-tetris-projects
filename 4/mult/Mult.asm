// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)
// The algorithm is based on repetitive addition.

@i
M=1
@R2
M=0
(LOOP)
   @R1
   D=M
   @i
   D=M-D
   @STOP
   D;JGT // if i > n, goto STOP; aka if i-n >= 0

   @R0
   D=M
   @R2
   M=D+M // add R0 to the product

   @i
   M=M+1 // add one to i

   @LOOP
   0;JMP // goto LOOP

(STOP)
   @STOP
   0;JMP