﻿($define! $sequence-core
((wrap ($vau ($seq2) #ignore ($seq2 ($define! $aux ($vau (head . tail) env ($if (null? tail) (eval head env)
($seq2 (eval head env) (eval (cons $aux tail) env))))) ($vau body env ($if (null? body) #inert
(eval (cons $aux body) env)))))) ($vau (first second) env ((wrap ($vau #ignore #ignore (eval second env))) (eval first env)))))