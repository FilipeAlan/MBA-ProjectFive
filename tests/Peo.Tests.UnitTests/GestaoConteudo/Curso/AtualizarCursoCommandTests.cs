using Xunit;

using Peo.GestaoConteudo.Application.UseCases.Curso.Atualizar;

namespace Peo.Tests.UnitTests.GestaoConteudo.Curso;

public class AtualizarCursoCommandTests
{
    [Fact]
    public void Ctor_Deve_inicializar_campos_padrao()
    {
        // act
        var cmd = new AtualizarCursoCommand();

        // assert
        Assert.NotNull(cmd);
        Assert.Equal(string.Empty, cmd.Titulo);
        Assert.NotNull(cmd.Tags);
        Assert.Empty(cmd.Tags);
    }
}
