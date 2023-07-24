using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using manualdojovem.Models;
using System.IO;
using manualdojovem.Data;

namespace manualdojovem.Data
{
    public class Contexto : IdentityDbContext<User>
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* FluenteAPI */
            #region Identity Setting
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable(name: "UserTokens");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });
            #endregion

            #region Populate Roles 
            var roles = new List<IdentityRole>(){
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR"
                },
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Moderador",
                    NormalizedName = "MODERADOR"
                },
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Usuario",
                    NormalizedName = "USUARIO"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            #endregion

            #region Populate Admin
            var hash = new PasswordHasher<User>();
            byte[] avatarPic = File.ReadAllBytes(
                Directory.GetCurrentDirectory() + @"\wwwroot\img\avatar.png");
            var users = new List<User>(){
                new User{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Jovens Tristes",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@manualdojovemadulto.com",
                    NormalizedEmail = "ADMIN@MANUALDOJOVEMADULTO.COM",
                    EmailConfirmed = true,
                    PasswordHash = hash.HashPassword(null, "123456"),
                    SecurityStamp = hash.GetHashCode().ToString(),
                    ProfilePicture = avatarPic,
                    BirthDate = DateTime.Parse("01/01/2005")
                }
            };
            modelBuilder.Entity<User>().HasData(users);
            #endregion

            #region Populate User Role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[0].Id
                },
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[1].Id
                },
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[2].Id
                }
            );
            #endregion

            #region Populate Categories
            List<Category> categories = new List<Category>(){
            new Category {
               Id = 1,
               Name = "Culinária"
            },
            new Category {
               Id = 2,
               Name = "Cuidados Pessoais e Estilos"
            },
            new Category {
               Id = 3,
               Name = "Finanças"
            },
            new Category {
               Id = 4,
               Name = "Mecânica"
            },
            new Category {
               Id = 5,
               Name = "Pets"
            },
            new Category {
               Id = 6,
               Name = "Saúde e Primeiros Socorros"
            }
        };
            modelBuilder.Entity<Category>().HasData(categories);
            #endregion


            #region Populate Articles
            List<Article> articles = new List<Article>(){

            new Article {
               Name = "Como Fazer Pastel",
               Id = 1,
               CategoryId = 1,
               Description = "Ingredientes:\n"+
                  "3 xícaras de farinha de trigo\n"+
                  "1 xícara de água morna (ou um pouco mais)\n"+
                  "3 colheres (sopa) de óleo (de soja, milho, girassol ou algodão)\n"+
                  "1 colher (sopa) de aguardente\n"+
                  "1 colher (sopa) rasa de sal\n"+
                  "farinha de trigo para trabalhar a massa\n"+
                  "\n"+
                  "Modo de preparo:\n"+
                  "Coloque a farinha misturada com o sal em uma vasilha ou uma mesa e abra um buraco no meio.\n"+
                  "Nesse buraco, coloque o óleo, a aguardente e um pouco de água.\n"+
                  "Misture a água e a farinha aos poucos, cada vez pegando um pouco mais de farinha da borda do buraco.\n"+
                  "Quando a massa estiver ficando dura, coloque mais água.\n"+
                  "A massa deve ficar macia.\n"+
                  "Se estiver um pouco grudenta, não tem problema.\n"+
                  "Se estiver muito grudenta, coloque mais farinha.\n"+
                  "Se estiver dura, coloque mais água.\n"+
                  "Em uma superfície enfarinhada, abra a massa com o auxílio de um rolo, de forma que ela fique bem fina.\n"+
                  "Se não ficar fina, ela não fica crocante depois de fritar.\n"+
                  "Recheie a gosto, e feche com um garfo ou com o verso de uma faca.\n"+
                  "Frite em óleo quente (não muito) em fogo médio-alto e escorra com o auxílio de uma escumadeira, antes de deixar para secar em papel absorvente.\n",
               Image = @"\img\articles\Banner-do-TCC15.png",
            },
            new Article {
               Id = 2,
               CategoryId = 1,
               Name = "Como Fazer Arroz",
               Description = "Ingredientes:\n"+
                  "2 xícaras (chá) de arroz\n"+
                  "2 colheres (sopa) de óleo de soja\n"+
                  "2 dentes de alho amassados\n"+
                  "1/2 unidade de cebola picada\n"+
                  "Sal a gosto\n"+
                  "\n"+
                  "Modo de Preparo:\n"+
                  "1. Leve uma panela ao fogo, deixe-a aquecer e junte o óleo.\n"+
                  "2. Despeje a cebola e refogue até que fique transparente.\n"+
                  "3. Junte o alho e deixe o refogado dourar.\n"+
                  "4. Adicione o arroz e refogue-o, até que esteja seco - vai sentir, pois os grãos se soltam e, ao mexer, percebe isso, até mesmo pelo som que faz o arroz ao ser mexido na panela.\n"+
                  "5. Junte 4 xícaras (chá) de água fervente.\n"+
                  "6. Deixe levantar fervura novamente e coloque sal.\n"+
                  "7. Tampe a panela, mas não totalmente.\n"+
                  "8. Quando a água chegar com mesmo nível do arroz, feche a tampa por completo e abaixe o fogo.\n"+
                  "9. Assim que a água secar totalmente já está pronto.\n"+
                  "10. Deixe o arroz descansar por 5 minutinhos antes de comer.\n",
               Image = @"\img\articles\Banner-do-TCC9.png",
            },
            new Article {
               Id = 3,
               CategoryId = 6,
               Name = "Como lidar com alguém com crise de ansiedade",
               Description = "1.	Ajude a ritmar a respiração\n"+
                     "2.	Evite reações raivosas\n"+
                     "3.	Contatos físicos mais delicados\n"+
                     "4.	Pergunte se quer falar\n"+
                     "5.	Tente distrair com assuntos positivos\n"+
                     "6.	Sair com a pessoa para dar uma volta\n",
               Image = @"\img\articles\Banner-do-TCC13.png",
            },
            new Article {
               Id = 4,
               CategoryId = 6,
               Name = "Comportamentos suicidas que você pode identificar",
               Description = "1 - Pensamentos\n"+
                  "Pensamentos remoídos obsessivamente, sem esperança e concentração são um dos primeiros indícios, assim como enxergar a vida como algo sem sentido ou propósito;\n"+
                  "2 - Humor\n"+
                  "Alterações extremas no humor podem sinalizar emoções suicidas. Excesso de raiva, sentimento de vingança, ansiedade, irritabilidade e sentimentos intensos de culpa ou vergonha são sinais aos quais você deve ficar atento;\n"+
                  "3 - Avisos\n"+
                  "Frases como “a vida não vale a pena”, “estou tão sozinho que queria morrer” ou “você vai sentir a minha falta” estão diretamente ligadas a pensamentos sobre a morte. Se a pessoa se sente um fardo, busque ajuda;\n"+
                  "4 - Melhora súbita\n"+
                  "Geralmente a ideia de suicídio está ligada a um sentimento de que a pessoa está no fundo poço. A felicidade súbita pode ser um sinal de que a pessoa já aceitou a decisão de encerrar a própria vida. Caso você perceba uma melhora repentina, busque ajuda imediatamente;\n"+
                  "5 - Desapego\n"+
                  "Caso você perceba que a pessoa está começando a “fechar pontas soltas”, doar seus pertences e até visitar vários entes queridos, faça uma intervenção o mais rápido possível;\n"+
                  "6 - Irresponsabilidade\n"+
                  "Comportamentos irresponsáveis e perigosos, sem medir as consequências, como o uso excessivo de álcool e drogas, direção imprudente e sexo sem proteção são indícios de que a pessoa já não dá a importância devida a própria vida;\n"+
                  "7 - Mudança na rotina\n"+
                  "Todo mundo tem um lugar que gosta de frequentar em especial, então repare em mudanças extremas na rotina. Caso a pessoa pare de ir a locais que sempre gostou de visitar, tome uma medida o mais rápido possível. Abandonar atividades que lhe davam prazer é um grande sinal de alerta.\n"+
                  "Caso sinta algum desses sintomas ligue 188.\n",
               Image = @"\img\articles\Banner-do-TCC12.png",
            },
            new Article {
               Id = 5,
               CategoryId = 2,
               Name = "Tons de Cores",
               Description = "As cores têm o poder de deixar o ambiente mais alegre e descontraído, além de expressar nossa personalidade\n"+
               "Antes de criar as combinações precisamos entender como as cores funcionam. Para isso, precisamos relembrar as cores primárias, secundárias e terciárias. Elas são a base do círculo cromático que é um tipo de tabela de combinação de cores, sendo elas, vermelho, amarelo e azul.\n"+
               "A partir dessas combinações, temos o círculo cromático com 12 cores. Você vai perceber que essa tabelinha tem diversas variações de cor, esses diferentes tons são definidos pelas porcentagens dessas misturas. Assim, temos infinitas possibilidades de combinações.",
               Image = @"\img\articles\Banner-do-TCC.png",
            },
            new Article {
               Id = 6,
               CategoryId = 2,
               Name = "Moda Sustentável",
               Description = "Moda sustentavel é aquela que não é passageira e sim a que veio para ficar, idependente da epoca sempre sera uma boa escolha, e isso é muito importante para o ambiente por diminuir a poluição da produção textil.\n"+
                  "Como, por exemplo, a escolha de peças sem estampa, que não ficam muito marcadas e servem para quase todas ocasiões, assim se mantendo no guarda roupas por mais tempo e se tornando uma peça atemporal.",
               Image = @"\img\articles\Banner-do-TCC1.png",
            },
            new Article {
               Id = 7,
               CategoryId = 4,
               Name = "Como desentupir uma privada",
               Description = "Com Desentupidor\n"+
                  "Para utilizar o desentupidor basta segurar no cabo e bombear a água da privada até que o objeto ali preso consiga escapar. Certifique-se de ter desligado o registro de água antes de começar a tentativa.\n"+

                  "Bicarbonato e vinagre\n"+
                  "É preciso misturar 1/2 copo de bicarbonato de sódio com 1/2 copo de vinagre e jogar o conteúdo diretamente na privada. Espere agir brevemente e depois tente ativar a descarga.\n"+

                  "Água Quente\n"+
                  "Encha um balde com um litro de água quente – pode ser do chuveiro, da banheira ou até mesmo aquecida no fogão.\n"+
                  "Despeje todo o conteúdo do balde diretamente no vaso sanitário e aguarde.\n",
               Image = @"\img\articles\Banner-do-TCC8.png",
            },
            new Article {
               Id = 8,
               CategoryId = 2,
               Name = "Cortes de Cabelos",
               Description = "Quando falamos de estilo, o cabelo é um dos assuntos mais importantes, ja que é o mais aparente e mais marcante, por isso sempre mantenha ele bem aparado e limpo.\n"+
               "O corte favorito dos homens é o Tape Fade que é um corte bem moderno e com um grande estilo que melhora exponencialmente a aparecia alheia.\n"+
               "Enquanto para as mulheres, o Shaggy hair é bem famoso e estiloso, mantendo uma aparência mais jovem e despojada.",
               Image = @"\img\articles\Banner-do-TCC2.png",
            },
            new Article {
               Id = 9,
               CategoryId = 5,
               Name = "Escolhendo um Pet",
               Description = "Ao escolher um pet temos que levar em consideração o nosso estilo de vida, não adianta ter um cachorro e não ter espaço para ele brincar, ele se tornará um cão depressivo e sedentario.",
               Image = @"\img\articles\Banner-do-TCC5.png",
            },
            new Article {
               Id = 10,
               CategoryId = 5,
               Name = "Cuidados com um Pet",
               Description = "Um pet é mais frágil que nós humanos e por isso precisamos de uma atenção extra quando se trata da saúde dele, levando ele no veterinario periodicamente e mantendo uma boa alimentação com comidas proprias.",
               Image = @"\img\articles\Banner-do-TCC6.png",
            },
            new Article {
               Id = 11,
               CategoryId = 6,
               Name = "Primeiros socorros",
               Description = "Fazer massagem cardíaca.\n"+
                  "Desengasgar.\n"+
                  "Estancar sangramentos.\n"+
                  "Amenizar queimaduras.\n"+
                  "Desafogar.\n"+
                  "Fazer transporte de vítimas.\n"+
                  "Cuidar de fraturas ósseas.\n",
               Image = @"\img\articles\Banner-do-TCC3.png",
            },
            new Article {
               Id = 12,
               CategoryId = 5,
               Name = "Integração do Pet",
               Description = "Nenhum comportamento é genetico, todos são costumes e ensinamentos, por isso é bom relacionar o pet com outros desde pequenos, para criar um animal mais dócil e social.",
               Image = @"\img\articles\Banner-do-TCC7.png",
            },
            new Article {
               Id = 13,
               CategoryId = 3,
               Name = "Plano de Economia",
               Description = "O mercado financeiro ficou cada vez mais competitivo, e com isso, é preciso mais cuidado e pesquisa na hora de abrir uma conta corrente. Para te ajudar com isso, vamos falar hoje de algumas dicas que você pode priorizar na hora da escolha, principalmente se você a utilizará para receber o seu empréstimo consignado.\n"+
                  "Se você recebe por cartão magnético, deve saber que para fazer um empréstimo consignado é necessário ter um conta corrente ou poupança em nome do titular do benefício\n"+
                  "Os bancos digitais têm se mostrado fortes concorrentes em relação aos bancos tradicionais. Além da praticidade na hora de abrir a conta (15 a 20 minutos), os bancos digitais também costumam serem isentos de taxas, ou oferecem taxas mais acessíveis.\n"+
                  "Já os bancos tradicionais podem ser mais burocráticos na hora de abrir uma conta, sendo necessário que você vá presencialmente até à agência, e podem cobrar taxas maiores.\n"+
                  "Uma pessoa pode ser considerada consumista quando dá preferência ao shopping a qualquer outro tipo de passeio, faz compras até que todo o limite de crédito que possui exceda, deixa de usar objetos comprados há algum tempo, não consegue sair do shopping sem comprar algo.\n"+
                  "O consumismo é fortemente induzido pelo marketing que consegue atingir a fragilidade íntima das pessoas e este é um dos motivos pelos quais o sexo feminino é mais propenso à compulsão",
               Image = @"\img\articles\Banner-do-TCC4.png",
            },
            new Article {
               Id = 14,
               CategoryId = 4,
               Name = "Como trocar a resistência de um chuveiro",
               Description = "1. Posicione a escada próximo ao chuveiro, suba e, com ajuda da chave de fenda, abra o chuveiro. Se preferir, tire o chuveiro de sua posição e o coloque em cima de uma mesa.\n"+
                  "2. Em seguida, abra o chuveiro e verifique a posição da resistência queimada. Uma dica útil para não se esquecer de como a peça estava posicionada é tirar uma foto com o celular.\n"+
                  "3. Retire a resistência queimada e substitua pela nova.\n"+
                  "4. Feche o chuveiro e coloque de volta no lugar, se tiver tirado.\n"+
                  "5. Ainda com o chuveiro desligado, abra o chuveiro e deixe a água correr por alguns minutos.\n"+
                  "6. Desligue o chuveiro, ligue o disjuntor, e ligue novamente o chuveiro para verificar se a resistência está funcionando\n",
               Image = @"\img\articles\Banner-do-TCC10.png",
            },
            new Article {
               Id = 15,
               CategoryId = 4,
               Name = "Como trocar uma lâmpada",
               Description = "Desligue o interruptor e o disjuntor no quadro elétrico para que não haja problema com choques;\n"+
                  "Verifique a voltagem da lâmpada que deve ser trocada. Tenha sempre algumas reservas guardadas em casa;\n"+
                  "Pegue uma escada ou uma cadeira para alcançar a lâmpada. Nunca faça esse serviço com os pés descalços;\n"+
                  "Pegue um pano seco, envolva sobre a lâmpada e gire-a com cuidado no sentido anti-horário. O pano protegerá a sua mão caso o vidro quebre.\n"+
                  "A lâmpada pode estar quente, então cuidado para não se queimar. Caso ela ainda esteja quente, aguarde alguns minutos até que esfrie completamente;\n"+
                  "Para retirá-la, não toque na rosca ou na parte metálica;\n"+
                  "Pegue a lâmpada nova (com a mesma voltagem e potência) e coloque-a no bocal, girando no sentido horário até sentir que ela ficou firme;\n"+
                  "Já pode ligar o disjuntor no quadro de luz e o interruptor para testar o serviço\n",
               Image = @"\img\articles\Banner-do-TCC11.png",
            },
            new Article {
               Id = 16,
               CategoryId = 3,
               Name = "Estilo de vida consumista",
               Description = "Pensar em morar longe da familia pode parecer legal, mas as vezes é surreal, levando em conta o seu salario e o quanto esta disponivel para gastar, por isso tenha sempre uma quantia boa reservada em casos de emergencia e pense em morar sozinho apenas quando for realmente necessario, assim se matendo acolhido por seus responsaveis e tendo apoio em sentido emocional e financeiro, assim sendo possivel guardar uma parte maior do seu dinheiro para futuros plano",
               Image = @"\img\articles\Banner-do-TCC17.png",
            },
            new Article {
               Id = 17,
               CategoryId = 1,
               Name = "Como fazer feijão",
               Description = "Ingredientes:\n"+
                  "1 xícara (chá) de feijão-carioquinha cru (170 g)\n"+
                  "4 xícaras (chá) de água (800 ml)\n"+
                  "1 sachê de tempero pronto\n"+
                  "1 colher (chá) de sal\n"+
                  "1 folha de louro\n"+
                  "1 colher (sopa) de óleo\n"+
                  "2 dentes de alho amassados\n"+
                  "\n"+
                  "Modo de preparo:\n"+
                  "Deixe o feijão de molho por 2 horas.\n"+
                  "Escorra e transfira para uma panela de pressão.\n"+
                  "Junte a água, o tempero pronto, o sal e o louro.\n"+
                  "Deixe cozinhar, em fogo baixo, por 20 minutos após o início da fervura.\n"+
                  "Em uma frigideira média, coloque o óleo e leve ao fogo alto para aquecer. Junte o alho e refogue rapidamente até dourar.\n"+
                  "Adicione uma concha dos grãos do feijão cozido e amasse-os com uma colher.\n"+
                  "Volte o refogado à panela de pressão e deixe cozinhar, com a panela semi-tampada, por 10 minutos, ou até encorpar ligeiramente.\n"+
                  "Retire do fogo e sirva em seguida.\n",
               Image = @"\img\articles\Banner-do-TCC14.png",
            },
            new Article {
               Id = 18,
               CategoryId = 3,
               Name = "Como escolher um banco",
               Description = "Ao escolher um banco, temos que levar em conta o quanto ele nos beneficia, quais suas taxas e serviços, e isso é extremamente importante para invstimentos e segurança de seu dinheiro.\n" +
               "Tendo em vista que o banco é onde seu dinheiro esta guardado, o correto é escolher um banco tradicional que dificilmente lhe trará problemas, como o Banco do Brasil por exemplo, que esta há muitos anos no mercado.",
               Image = @"\img\articles\Banner-do-TCC16.png",
            }
            };
            modelBuilder.Entity<Article>().HasData(articles);
            #endregion
        }
    }
}