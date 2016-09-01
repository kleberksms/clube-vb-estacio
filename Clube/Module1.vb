Imports System.Text.RegularExpressions

Module Module1

#Region "variable"
    Dim _reader As String
    Dim _currentOption As Integer
    Private _clube As Clube
#End Region

#Region "Main"
    Sub Main()

        _clube = New Clube()
        _clube.Socios = New List(Of Socios)()
        _clube.Times = New List(Of Time)()

        Console.WriteLine("Olá, Bem vindo ao Clube do VBZão")
        Init()
        _reader = Console.ReadLine()

        While (Not _reader.IsOpcaoInit())
            Console.WriteLine("Não esta entre as opções")
            Init()
            _reader = Console.ReadLine()
        End While

        _currentOption = Integer.Parse(_reader)

        If (_currentOption.Equals(1)) Then
            CadastarNovoSocio()
            Console.WriteLine("Socio Cadastrado com sucesso")
            Console.WriteLine(_clube.Socios.Last())
        End If
        If (_currentOption.Equals(2)) Then
        End If
        If (_currentOption.Equals(3)) Then
        End If

    End Sub
#End Region

#Region "Inicialização"
    Public Sub Init()
        Console.WriteLine("Digite:")
        Console.WriteLine("1 - Para novo sócio")
        Console.WriteLine("2 - Para alterar o cadastro do sócio")
        Console.WriteLine("3 - Para cadastrar um time")
    End Sub

    <Runtime.CompilerServices.Extension>
    Public Function IsOpcaoInit(phrase As String) As Boolean
        Return VerifyOptions(phrase, 1, 3)
    End Function


#End Region

#Region "Novo Socio"
    Private Sub CadastarNovoSocio()
        Console.WriteLine("Cadastro De Sócios")
        Console.WriteLine("Informe o primeiro Nome:")
        Dim nome = Console.ReadLine()
        Console.WriteLine("Informe segundo Nome:")
        Dim sobrenome = Console.ReadLine()
        Console.WriteLine("Informe o cpf:")
        Dim cpf = Console.ReadLine()

        If (Not ParticipaDeAlgumEsporte()) Then
            _clube.Socios.Add(New Socios(New NomeCompleto(nome, sobrenome), cpf, Nothing, Mensalidade.PlanoBasico))
            Return
        End If

        _clube.Socios.Add(New Socios(New NomeCompleto(nome, sobrenome), cpf, DefineEsporte(), Mensalidade.PlanoBasico))
    End Sub

    Private Function DefineEsporte() As Esporte

        EscolheuEsporte()

        While Not _reader.IsOpcoesDeEsporte
            EscolheuEsporte()
        End While

        _currentOption = Integer.Parse(_reader)

        If (_currentOption.Equals(1)) Then
            Return Esporte.Natacao
        End If

        Return Esporte.Futebol

    End Function

    Public Sub EscolheuEsporte()
        Console.WriteLine("Escolha o esporte")
        Console.WriteLine("1 - Natação")
        Console.WriteLine("2 - Futebol")
        _reader = Console.ReadLine()
    End Sub

    <Runtime.CompilerServices.Extension>
    Public Function IsOpcoesDeEsporte(phrase As String) As Boolean
        Return VerifyOptions(phrase, 1, 2)
    End Function

    Private Function ParticipaDeAlgumEsporte() As Boolean

        Console.WriteLine("Participa de algum esporte? sim/não")

        Dim r = Console.ReadLine()

        While Not (String.Equals(r, "sim", StringComparison.CurrentCultureIgnoreCase) Or String.Equals(r, "não", StringComparison.CurrentCultureIgnoreCase))
            Console.WriteLine("Não entendi, participa de algum esporte? sim/não")
            r = Console.ReadLine()
        End While

        Return r.Equals("sim")

    End Function
#End Region

#Region "Shared"
    Private Function VerifyOptions(phrase As String, rangeInit As Integer, rageEnd As Integer) As Boolean
        Dim n As Integer
        Dim res As Boolean = Integer.TryParse(phrase, n)
        If Not res Then
            Return False
        End If
        Return Enumerable.Range(rangeInit, rageEnd).Contains(n)
    End Function
#End Region


End Module

Public Class Clube

    Public Times As List(Of Time)
    Public Socios As List(Of Socios)

End Class

Public Class Time

    Public Property Id As Guid
    Public Property Nome As String
    Public Property Esporte As Esporte
    Public Property Esportistas As List(Of Socios)

    Public Sub New(nome As String, esporte As Esporte)
        Id = New Guid()
        Me.Nome = nome
        Me.Esporte = esporte
    End Sub

End Class

Public Class Socios

    Public Property Id As Guid
    Public Property Nome As NomeCompleto
    Public Property Cpf As String
    Public Property Esporte As Esporte?
    Public Property ModalidadeMensalidade As Mensalidade
    Public Property DataCadastro As DateTime

    Public Sub New(nome As NomeCompleto, cpf As String, esporte As Esporte, modalidadeMensalidade As Mensalidade)
        Id = New Guid()
        Me.Nome = nome
        Me.Cpf = cpf
        Me.Esporte = esporte
        Me.ModalidadeMensalidade = modalidadeMensalidade
        DataCadastro = Now
    End Sub

    Public Overrides Function ToString() As String
        Dim concat = String.Empty
        concat += String.Format("Socio: {0} {1}",Nome, Environment.NewLine)
        concat += String.Format("Cadastrado em: {0} {1}",DataCadastro.ToString("dd/MM/yyyy"), Environment.NewLine)
        concat += String.Format("CPF: {0} {1}",Cpf, Environment.NewLine)
        concat += String.Format("Esporte: {0} {1}",Esporte, Environment.NewLine)
        concat += String.Format("Modalidade da Mensalidade: {0} {1}",ModalidadeMensalidade, Environment.NewLine)
        Return concat
    End Function
End Class

Public Class NomeCompleto

    Public Property PrimeiroNome As String
    Public Property SegundoNome As String

    Public Sub New(primeiroNome As String, segundoNome As String)
        Me.PrimeiroNome = primeiroNome
        Me.SegundoNome = segundoNome
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0} {1}", PrimeiroNome, SegundoNome)
    End Function
End Class

Public Enum Esporte
    Natacao
    Futebol
End Enum

Public Class Incricao

    Public Property Id As Guid
    Public Property Time As Time
    Public Property Valor As Decimal
    Public Property InscricaoPaga As Boolean

    Public Sub New(time As Time)
        Id = New Guid()
        Me.Time = time
    End Sub

    Public Function IsValid() As Boolean

        If (Time.Esporte.Equals(Esporte.Natacao)) Then
            Return Valor.Equals(15)
        End If
        If (Time.Esporte.Equals(Esporte.Natacao)) Then
            Return Valor.Equals(15)
        End If
        Return False

    End Function

End Class

Public Class Pagamento

    Public Sub PagarMensalidade()

    End Sub

    Public Sub PagarIncricaoEvento()

    End Sub

End Class

Public Enum Mensalidade
    PlanoBasico = 100
End Enum

