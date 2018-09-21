;map-core
($define! map-core
(wrap ($vau (appv . lss) env
($define! acc
($lambda (input (k1 k2) base-result head tail sum)
($define! aux
($lambda (input count)
($if (=? count 0)
base-result
(sum (head input)
(aux (tail input) (- count 1))))))
(aux input (+ k1 k2))))
($define! enlist
($lambda (input ms head tail)
($define! result (acc input ms () head tail cons))
(apply encycle! (list* result ms))
result))
($define! mss (cddr (get-list-metrics lss)))
($define! cars ($lambda (lss) (enlist lss mss caar cdr)))
($define! cdrs ($lambda (lss) (enlist lss mss cdar cdr)))
($define! result-metrics
(acc lss mss (cddr (get-list-metrics (car lss)))
($lambda (lss) (cddr (get-list-metrics (car lss))))
cdr
($lambda ((j1 j2) (k1 k2))
(list (max j1 k1)
($cond ((=? j2 0) k2)
((=? k2 0) j2)
(#t (lcm j2 k2)))))))
(enlist lss
result-metrics
($lambda (lss) (apply appv (cars lss) env))
cdrs))))