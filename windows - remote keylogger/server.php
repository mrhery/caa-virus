<?php

$o = fopen("php://input", "rb");
$str = stream_get_contents($o);
fclose($o);

$w = fopen("log.txt", "a+");
fwrite($w, $str);
fclose($w);