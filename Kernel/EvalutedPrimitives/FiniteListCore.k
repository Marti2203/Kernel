($define! finite-list-core?
($lambda args
(apply and? (map ($lambda (x) ($let (((#ignore n . #ignore) (get-list-metrics x))) (>? n 0)))
args))))