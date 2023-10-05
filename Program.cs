using System;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("\nInfluenza H5N1 - identificação de pessoas sintomáticas ");

        Console.WriteLine("Informe o seu nome:");
        string nome = Console.ReadLine();

        Console.WriteLine("Informe a sua idade:");
        int idade;
        while (!int.TryParse(Console.ReadLine(), out idade) || idade < 0)
        {
            Console.WriteLine("Idade inválida. Tente novamente");
        }

        bool cartaoVacinalemDia = Questionario("Seu cartão de vacina está em dia?");
        bool teveSintomasRecentes = Questionario("Teve algum dos sintomas recentemente? (dor de cabeça, febre, náusea, dor articular, gripe)");
        bool teveContatoComPessoainfectada = Questionario("Teve contato com pessoas com sintomas gripais nos últimos dias?");
        bool retornandoDeViagem = Questionario("Está retornando de viagem realizada no exterior?");

        double risco = CalcularRisco(cartaoVacinalemDia, teveSintomasRecentes, teveContatoComPessoainfectada, retornandoDeViagem);

        Console.WriteLine("\n -- Orientação identificada após análise do questionário: " + Orientacao(risco, retornandoDeViagem));

        ImprimirDados(nome, idade, cartaoVacinalemDia, teveSintomasRecentes, teveContatoComPessoainfectada, retornandoDeViagem, risco);

    }


    static bool Questionario(string questionario)
    {
        for (int tentativas = 0; tentativas < 3; tentativas++)
        {
            Console.WriteLine(questionario + " (SIM/NAO):");
            string resposta = Console.ReadLine().Trim().ToUpper();
            if (resposta == "SIM") return true;
            if (resposta == "NAO") return false;
        }

        Console.WriteLine("Não foi possível realizar o diagnóstico.");
        Console.WriteLine("Gentileza procurar ajuda médica caso apareça algum sintoma.");
        Environment.Exit(0);
        return false;
    }

    static double CalcularRisco(bool cartaoVacinalemDia, bool teveSintomasRecentes, bool teveContatoComPessoainfectada, bool retornandoDeViagem)
    {
        double porcentagem = 0.0;

        if (!cartaoVacinalemDia)
            porcentagem += 10.0;
        if (teveSintomasRecentes)
            porcentagem += 30.0;
        if (teveContatoComPessoainfectada)
            porcentagem += 30.0;
        if (retornandoDeViagem)
            porcentagem += 30.0;

        return porcentagem;
    }

    static string Orientacao(double risco, bool retornandoDeViagem)
    {
        if (retornandoDeViagem)
            return "Você ficará sob observação por 05 dias.";

        if (risco <= 30.0)
            return "Paciente sob observação. Caso apareça algum sintoma, gentileza buscar assistência médica.";
        else if (risco <= 60.0)
            return "Paciente com risco de estar infectado. Gentileza aguardar em lockdown por 02 dias para ser acompanhado.";
        else if (risco <= 89.0)
            return "Paciente com alto risco de estar infectado. Gentileza aguardar em lockdown por 05 dias para ser acompanhado.";
        else if (risco >= 90.0)
            return "Paciente crítico! Gentileza aguardar em lockdown por 10 dias para ser acompanhado.";
        else
            return "Orientação inválida";
    }

    static void ImprimirDados(string nome, int idade, bool cartaoVacinalemDia, bool teveSintomasRecentes, bool teveContatoComPessoainfectada, bool retornandoDeViagem, double risco)
    {
        Console.WriteLine("Nome: " + nome);
        Console.WriteLine("Idade: " + idade);
        Console.WriteLine("Cartão de Vacina em Dia: " + (cartaoVacinalemDia ? "Sim" : "Não"));
        Console.WriteLine("Teve Sintomas Recentemente: " + (teveSintomasRecentes ? "Sim" : "Não"));
        Console.WriteLine("Teve Contato com Pessoas Infectadas: " + (teveContatoComPessoainfectada ? "Sim" : "Não"));
        Console.WriteLine("Retornando de Viagem: " + (retornandoDeViagem ? "Sim" : "Não"));
        Console.WriteLine("Probabilidade de Infecção: " + risco + "%");
    }

}
