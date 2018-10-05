($define! reduce-core
    ($let ()
        ($define! reduce-acyclic
        ($lambda (ls bin id)
        ($cond ((null? ls) id)
               ((null? (cdr ls)) (car ls)) 
               (#t (bin (car ls) (reduce-acyclic (cdr ls) bin id))))))
        ($define! reduce-n
        ($lambda (ls bin n)
        ($if (=? n 1) (car ls) (bin (car ls) (reduce-n (cdr ls) bin (- n 1))))))

        (wrap ($vau (ls bin id . opt) env
            ($define! fixenv ($lambda (appv) ($lambda x (apply appv x env))))
            ($define! bin (fixenv bin)) 
            ($let (((p n a c) (get-list-metrics ls)))
            ($if (=? c 0) (reduce-acyclic ls bin id)
            ($sequence
                ($define! (pre in post) (map fixenv opt))
                ($define! reduced-cycle (post (reduce-n (map pre (list-tail ls a)) in c)))
                ($if (=? a 0)
                reduced-cycle
                (bin (reduce-n ls bin a) reduced-cycle)))))))))