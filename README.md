# Exercício: Automação de Pastas para o Setor de RH

## Objetivo
Desenvolver uma aplicação em C# que monitore uma pasta específica e mova arquivos recém-criados para subpastas baseadas em uma data extraída do nome do arquivo. A aplicação será utilizada pelo setor de RH para organizar arquivos de entrevistas.

## Descrição
Você deve criar uma aplicação que monitore uma pasta de origem. Quando um novo arquivo for criado nesta pasta, a aplicação deve mover o arquivo para uma subpasta dentro de uma pasta de destino. A subpasta deve ser nomeada com base em uma data extraída do nome do arquivo. Se a data não puder ser extraída, o arquivo deve ser movido para uma subpasta chamada "Review".

## Cenário
O setor de RH recebe arquivos de entrevistas de candidatos. O nome dos arquivos segue o padrão NomeCandidato_yyyyMMdd.docx, onde NomeCandidato é o nome do candidato e yyyyMMdd é a data da entrevista. Por exemplo, MariaSilva_20230101.docx.

## Requisitos
1. **Monitoramento de Pasta:**
   - A aplicação deve monitorar uma pasta especificada para novos arquivos.
2. **Extração de Data:**
   - A data deve ser extraída do nome do arquivo utilizando uma expressão regular que procure por um padrão de data no formato yyyyMMdd (por exemplo, 20230101).
3. **Movimentação de Arquivos:**
   - Se a data for encontrada no nome do arquivo, o arquivo deve ser movido para uma subpasta dentro da pasta de destino com o nome da data.
   - Se a data não for encontrada, o arquivo deve ser movido para uma subpasta chamada "Review".
4. **Limpeza da Pasta de Destino:**
   - A pasta de destino deve ser limpa (todos os arquivos e subpastas devem ser deletados) antes de iniciar o monitoramento.
5. **Tratamento de Erros:**
   - A aplicação deve tratar possíveis erros durante a movimentação dos arquivos e exibir mensagens de erro apropriadas.

## Dicas
- Utilize a classe Regex para extrair a data do nome do arquivo.
- Utilize os métodos Directory.Exists, Directory.CreateDirectory, Directory.Delete, File.Move e Path.Combine para manipulação de arquivos e pastas.
- Lembre-se de habilitar e desabilitar os eventos de monitoramento conforme necessário.

## Exemplo de Uso
1. A aplicação é iniciada e começa a monitorar a pasta C:\WatchFolder.
2. Um novo arquivo chamado MariaSilva_20230101.docx é criado na pasta C:\WatchFolder.
3. A aplicação move o arquivo para a pasta C:\DestinationFolder\20230101\MariaSilva_20230101.docx.
4. Um novo arquivo chamado AnaSouza.docx é criado na pasta C:\WatchFolder.
5. A aplicação move o arquivo para a pasta C:\DestinationFolder\Review\AnaSouza.docx.

## Dica extra
Você consegue usar o FileSystemWatcher, segue exemplo:

```csharp
private void InitializeWatcher()
{
    _fileSystemWatcher = new FileSystemWatcher(_watchFolder)
    {
        NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
    };

    _fileSystemWatcher.Created += OnFileCreated;
    _fileSystemWatcher.EnableRaisingEvents = true;
}
```