;list*-core
($define! list*
(wrap ($vau args #ignore
($sequence
($define! aux
(wrap ($vau ((head . tail)) #ignore
($if (null? tail)
head
(cons head (aux tail))))))
(aux args)))))