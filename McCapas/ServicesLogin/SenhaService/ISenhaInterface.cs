﻿namespace McCapas.ServicesLogin.SenhaService
{
    public interface ISenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}
