;list*-special
($define! list*-special
($lambda (head . tail)
($if (null? tail)
head
(cons head (apply list* tail)))))