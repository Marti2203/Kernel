($define! $sequence-special
        ($let (($seq2 ($vau (first second) env (($lambda #ignore (eval second env)) (eval first env)))))
        ($letrec ((aux (wrap ($vau (head . tail) env ($if (null? tail) (eval head env) ($seq2 (eval head env) (apply aux tail env)))))))
        ($vau body env ($if (null? body) #inert (apply aux body env))))))