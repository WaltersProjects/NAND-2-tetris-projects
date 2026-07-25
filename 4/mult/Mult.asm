// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)
// The algorithm is based on repetitive addition.

@R2
M=0

(LOOP)
   @R1
   D=M
   @END
   D;JEQ // if i = 0, goto END

   @R0
   D=M
   @R2
   M=D+M // add R0 to the product

   @R1
   M=M-1 // subtract one from i

   @LOOP
   0;JMP // loop

(END)
   @END
   0;JMP