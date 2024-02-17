<?php

$o = fopen("php://input", "rb");
$str = stream_get_contents($o);
fclose($o);

$filename = uniqid() . ".jpg";
$w = fopen($filename, "w+");
fwrite($w, base64_decode($str));
fclose($w);