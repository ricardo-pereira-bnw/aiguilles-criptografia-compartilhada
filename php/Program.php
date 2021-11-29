<?php

$plaintext = 'Ricardo Pereira Dias';
$password = '3sc3RLrpd17';
$method = 'aes-256-cbc';

// Must be exact 32 chars (256 bit)
$key = substr(hash('sha256', $password, true), 0, 32);

// IV must be exact 16 chars (128 bit)
$iv = chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x0);

// av3DYGLkwBsErphcyYp+imUW4QKs19hUnFyyYcXwURU=
$encrypted = base64_encode(openssl_encrypt($plaintext, $method, $key, OPENSSL_RAW_DATA, $iv));

// My secret message 1234
$decrypted = openssl_decrypt(base64_decode($encrypted), $method, $key, OPENSSL_RAW_DATA, $iv);

echo "\n\n";
echo "-------------------------------------------------\n";
echo "PHP\n";
echo "-------------------------------------------------\n";
echo "Cifrador: AES 256 CBC\n";
echo "Chave: {$password}\n";
echo "Texto: {$plaintext}\n";
echo "Cifrado: {$encrypted}\n";
echo "Decifrado: {$decrypted}\n";
echo "\n";

