namespace PictureAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            context.Profiles.AddOrUpdate(
                p => p.Name,
                new Models.Profile
                {
                    Name = "Type A Personality",
                    Description = "People living with type A personality are independent and stand out wherever they go, are aware of the importance of positive attitude, motivation and the establishment of a goal to follow in life. They are competitive by nature and recognized for their mind. They know when to take risks and are good entrepreneurs. They have a pragmatic character and easily solve problems when something blocks their way to success. This kind of people is opened to anything new and meets changing  with opened arms - be it spiritual or even technological. The secret of these successful people lies in the fact that they are not afraid to show off their own abilities and colorful personality.",
                    Keywords = "independent,stand out,positive attitude, motivation,goal,competitive,recognized,mind,takes risks,good entrepreneur,fun,intuitive,optimistic,control,power,pragmatic,solve problems,opened,spiritual,technological changing,not afraid,colorful personality,extroverted,confident,ambitious,creativity,energy,intimidating"
                },
                new Models.Profile
                {
                    Name = "Type B Personality",
                    Description = "Individuals living with type B personality are noted for having a lower level of stress. They usually work consistently, enjoy achievements and tend not to consider physical or mental stress when they do not reach the desired standards. People with type B personality tend to be more tolerant of others, are more relaxed than type A personality, more reflexive, have lower levels of anxiety, and have higher levels of imagination and creativity. When faced with competition, they can focus less on gain or loss and more on the joy of joining the game. These are born to be good communicators, being considered full of charisma and humor. They often think of the outer and inner world.",
                    Keywords = "low stress,work consistently,enjoys achievements,relaxed,tolerant,reflexive,low anxiety,imagination,creativity,happy,stable,balanced,caring,indifferent,impartial,careful,good communicator,charming,charismatic,humor,friendly,optimistic,confident,attention,approachable,extroverted,sociable,compassionate,honest,detached,neutral,wise,control"
                },
                new Models.Profile
                {
                    Name = "Type C Personality",
                    Description = "People with personality type C are not particularly assertive, so they do not talk about their pleasures and can not support the needs of others if it involves ignoring their own. As a long term effect, this can lead to stress or even depression. They have a passion for details and are very focused. They like to understand how things work. They tend to take life seriously, and this makes them excellent workers, but they can become extreme to the point of perfection. They may become hard to work with and so they will start spending time alone, becoming introverted. However, their passion for details provides excellent analytical skills, which means they are very intelligent and trustworthy.",
                    Keywords = "not assertive,not talk,stress,depression,passion for details,focused,understand,serious,excellent workers,analyze,methodical,practical,calm,sincere,honest,profound,responsible,careful,control,extreme,perfection,hard to work with,alone,introverted,analytical skills,intelligent,trustworthy,trusted,determined,perfectionists,anxiety,reliable,inflexible,introspective,impartial"
                },
                new Models.Profile
                {
                    Name = "Type D Personality",
                    Description = "Persons living with type D personality are characters that adapt hard to change and prefer almost always to live in a traditional way. They do not take very large responsibilities and do not like to risk it. Through the power of motivation and with the help of others, they can face their fears and become better in life. Representing 20% of the world's population, those with type D are affected by negative attitudes - worries, emotional instability, irritability. Because of this they have the greatest chance of having heart problems. Individuals with a type D personality have the tendency to not share their emotions with others because of their fear of rejection or disapproval. Having a low self esteem is the primary reason that prevents them from opening up to others.",
                    Keywords = "adapt hard,traditional,rituals,avoid responsibilities,avoid risks,fears,negative attitudes,worries,emotional instability,critical of themselves,perfectionists,depression,irritability,heart problems,not share their emotions,fear of rejection,disapproval,low self esteem, not opened,impulsive,anxiety,conservative,inflexible,indecisive"
                }
            );
            context.SaveChanges();

            context.Colors.AddOrUpdate(
                c => c.Name,
                new Models.Color
                {
                    Name = "Red",
                    Description = "The color red is a warm and positive color associated with our most physical needs and our will to survive. It exudes a strong and powerful masculine energy. Red is energizing. It excites the emotions and motivates us to take action. It signifies a pioneering spirit and leadership qualities, promoting ambition and determination.It is also strong-willed and can give confidence to those who are shy or lacking in will power. Being the color of physical movement, the color red awakens our physical life force.",
                    PersonalityTraits = "They are known as extroverted and optimistic, courageous and confident. They are action oriented and physically active, having strong survival instincts. They like to be the center of attention. They are ambitious and competitive and like to be the winner - they are achievement orientated and second place is not good enough for them. With them it is all or nothing.",
                    Keywords = "extroverted,optimistic,courageous,confident,attention,ambitious,competitive,energy"
                },
                new Models.Color
                {
                    Name = "Orange",
                    Description = "The color orange radiates warmth and happiness, combining the physical energy and stimulation of red with the cheerfulness of yellow. Orange relates to gut reaction as opposed to the physical reaction of red or the mental reaction of yellow. Orange offers emotional strength in difficult times. The color psychology of orange is optimistic and uplifting, rejuvenating our spirit. In fact orange is so optimistic and uplifting that we should all find ways to use it in our everyday life.",
                    PersonalityTraits = "They are warm, optimistic, extroverted and often flamboyant. They are friendly, good-natured and generally agreeable persons. They are assertive and determined rather than aggressive. They thrive on human social contact and social gatherings, bringing all types together. While they are charming and sociable they do tend to be a show-off.",
                    Keywords = "warm,optimistic,extroverted,flamboyant,friendly,good-natured,agreeable,assertive,determined,charming,sociable"
                },
                new Models.Color
                {
                    Name = "Yellow",
                    Description = "The color yellow relates to acquired knowledge. It is the color which resonates with the left or logic side of the brain stimulating our mental faculties and creating mental agility and perception. Being the lightest hue of the spectrum, the color psychology of yellow is uplifting and illuminating, offering hope, happiness, cheerfulness and fun. In the meaning of colors, yellow inspires original thought and inquisitiveness.",
                    PersonalityTraits = "They have a happy disposition and are cheerful and fun to be with. They are creative, often being the ones who come up with new ideas and they tend to have their head in the clouds much of the time. They can be very critical of themselves as well as others - they are perfectionists.They analyze everything, all the time, and are methodical in their thinking.They are impulsive and make quick decisions, but, out of anxiety, jump in too quickly and rush things.",
                    Keywords = "happy,cheerful,fun,creativity,critical of themselves,perfectionists,analyze,methodical,impulsive,quick decisions,anxiety"
                },
                new Models.Color
                {
                    Name = "Green",
                    Description = "The color green relates to balance and harmony. From a color psychology perspective, it is the great balancer of the heart and the emotions, creating equilibrium between the head and the heart. From a meaning of colors perspective, green is also the color of growth, the color of spring, of renewal and rebirth. It renews and restores depleted energy. It is the sanctuary away from the stresses of modern living, restoring us back to a sense of well being. This is why there is so much of this relaxing color on the earth, and why we need to keep it that way.",
                    PersonalityTraits = "They are a practical, down-to-earth persons with a love of nature. They are stable and well balanced or are striving for balance - in seeking this balance, they can at times become unsettled and anxious. They are kind, generous and compassionate - good to have around during a crisis as they remain calm and take control of the situation until it is resolved. They  are caring and nurturing to others - however they must be careful not to neglect their own needs while giving to others. They are intelligent  love to learn, quickly understanding new concepts.",
                    Keywords = "practical,nature,stable,balanced,unsettled,anxiety,kind,generous,compassionate,calm,solve problems,caring,intelligent,learn,understand"
                },
                new Models.Color
                {
                    Name = "Blue",
                    Description = "This color is one of trust, responsibility, honesty and loyalty. It is sincere, reserved and quiet, and doesn't like to make a fuss or draw attention. It hates confrontation, and likes to do things in its own way. From a color psychology perspective, blue is reliable and responsible. This color exhibits an inner security and confidence. You can rely on it to take control and do the right thing in difficult times.",
                    PersonalityTraits = "They are conservative, reliable and trustworthy - they are quite trusting of others although they are very wary in the beginning until they are sure of the other person. At the same time, they also have a deep need to be trusted. They are not impulsive or spontaneous - they always think before they speak and act and do everything at their own pace in their own time. They take time to process and share their feelings, being genuine and sincere.",
                    Keywords = "conservative,traditional,reliable,trustworthy,trusted,think,process,genuine,sincere"
                },
                new Models.Color
                {
                    Name = "Purple",
                    Description = "The color violet is the color of intuition and perception and is helpful in opening the third eye. It promotes deep concentration during times of introspection and meditation, helping you achieve deeper levels of consciousness. It relies on intuition rather than gut feeling. Powerful and dignified, violet conveys integrity and deep sincerity.",
                    PersonalityTraits = "They are honest, compassionate and understanding. Integrity is extremely important to them. They need structure in their life - organization is important to them and they can be quite inflexible when it comes to order in their life. They love rituals and traditions and look to the past when planning for the future. They are conscientious and reliable - a good person to have around in a crisis.",
                    Keywords = "honest,compassionate,understand,integrity,structure,inflexible,order,rituals,traditional,conscientious,reliable"
                },
                new Models.Color
                {
                    Name = "Cyan",
                    Description = "The color cyan helps to open the lines of communication between the heart and the spoken word. It presents as a friendly and happy color enjoying life. In color psychology, cyan controls and heals the emotions creating emotional balance and stability. In the process it can appear to be on an emotional roller coaster, up and down, until it balances itself. It radiates the peace, calm and tranquility of blue and the balance and growth of green with the uplifting energy of yellow.",
                    PersonalityTraits = "They are friendly and approachable, easy to communicate with. They are compassionate, empathetic and caring. They have a heightened sense of creativity and sensitivity. They speak from the heart and love sharing your inner most thoughts. They usually have highly developed intuitive abilities. They seek spiritual fulfillment, and they are often an evolved or old soul.",
                    Keywords = "friendly,approachable,good communicator,compassionate,empathetic,caring,creativity,imagination,sensitivity,intuitive,spiritual,evolved"
                },
                new Models.Color
                {
                    Name = "Light gray",
                    Description = "Represents prestige, health, energy, but also prosperity and balance.",
                    PersonalityTraits = "They are intuitive and profound, have superior spiritual orientations, are introspective, often occupied by the problems of the world, open to the new experiences they encounter, and often take the best decisions through the sense of responsibility they have.",
                    Keywords = "intuitive,profound,spiritual,introspective,opened,responsible"
                },
                new Models.Color
                {
                    Name = "Dark gray",
                    Description = "Represents maturity and responsibility, but also conservatism, boredom and depression.",
                    PersonalityTraits = "They are often neutral about life, to the point of becoming indifferent, tend to be indecisive, having a great need to create balance, to be rewarded, recognized and find their place. They are practical and calm, do not like to attract attention and are simply seeking a contented life.",
                    Keywords = "neutral,indifferent,indecisive,anxiety,depression"
                },
                new Models.Color
                {
                    Name = "Mid gray",
                    Description = "The color gray is an unemotional color. It is detached, neutral, impartial and indecisive. From a color psychology perspective, gray is the color of compromise - being neither black nor white, it is the transition between two non-colors. The closer gray gets to black, the more dramatic and mysterious it becomes. The closer it gets to silver or white, the more illuminating and lively it becomes. Being both motionless and emotionless, gray is solid and stable, creating a sense of calm and composure, relief from a chaotic world.",
                    PersonalityTraits = "They are trustworthy, stable, inspire security, wisdom and control in any situation. They can also be detached, neutral, impartial and indecisive.",
                    Keywords = "trustworthy,stable,security,wisdom,control,detached,neutral,impartial,indecisive"
                },
                new Models.Color
                {
                    Name = "White",
                    Description = "The color white is color at its most complete and pure, the color of perfection. The psychological meaning of white is purity, innocence, wholeness and completion. In color psychology white is the color of new beginnings, of wiping the slate clean, so to speak. It is the blank canvas waiting to be written upon. While white isn't stimulating to the senses, it opens the way for the creation of anything the mind can conceive.",
                    PersonalityTraits = "They are neat and immaculate in their appearance,having high standards of cleanliness and hygiene. They are far-sighted, with a positive and optimistic nature. They are well-balanced, sensible, discreet and wise. They are cautious, practical and careful with money. They think carefully before acting, being definitely not prone to impulsive behavior. They tend to have a great deal of self control.",
                    Keywords = "immaculate,impeccable,positive,optimistic,balanced,sensible,discreet,wise,cautious,practical,careful,control"
                },
                new Models.Color
                {
                    Name = "Black",
                    Description = "The color black relates to the hidden, the secretive and the unknown, and as a result it creates an air of mystery. It keeps things bottled up inside, hidden from the world. In color psychology this color gives protection from external emotional stress. It creates a barrier between itself and the outside world, providing comfort while protecting its emotions and feelings, and hiding its vulnerabilities, insecurities and lack of self confidence. Black is the absorption of all color and the absence of light. Black hides, while white brings to light. What black covers, white uncovers.",
                    PersonalityTraits = "Prestige and power are important to them. They are independent, strong-willed and determined and like to be in control of themselves and situations. They may be conservative and conventional and they may be too serious for their own good. They may appear intimidating to even their closest colleagues and friends, with an authoritarian, demanding and dictatorial attitude.",
                    Keywords = "prestige,power,independent,determined,control,conservative,conventional,traditional,serious,intimidating,authoritarian,demanding,dictatorial"
                }
            );
            context.SaveChanges();

            context.Types.AddOrUpdate(
                new Models.Type { Name = "Portrait", Description = "No description", PersonalityTraits = "-" },
                new Models.Type { Name = "Nature", Description = "No description", PersonalityTraits = "-" },
                new Models.Type { Name = "Abstract", Description = "No description", PersonalityTraits = "-" }
            );
            context.SaveChanges();

            context.Influences.AddOrUpdate(
                new Models.Influence { Name = "Expressionism", Description = "-", BeginYear = null, EndYear = null },
                new Models.Influence { Name = "Cubism ", Description = "-", BeginYear = null, EndYear = null },
                new Models.Influence { Name = "Modernism", Description = "-", BeginYear = null, EndYear = null }
            );
            context.SaveChanges();

            context.Galleries.AddOrUpdate(
                new Models.Gallery { Name = "All kind", Description = "Paintings belonging to all painters, influences and psychological profiles will be found here!" },
                new Models.Gallery { Name = "Vincent van Gogh", Description = "This gallery is dedicated to our beloved Vincent van Gogh! Add as many paintings as possible to help our van Gogh community grow!" },
                new Models.Gallery { Name = "Leonardo da Vinci", Description = "This gallery is dedicated to Leonardo da Vinci, so don't forget to contribute with as many as more paintings as possbile!" },
                new Models.Gallery { Name = "Nature", Description = "This gallery contains paintings that are most related to nature themes. Enjoy!" },
                new Models.Gallery { Name = "Portraits", Description = "This gallery contains portraits paintings from all the painters around. Enjoy!" },
                new Models.Gallery { Name = "Abstract", Description = "This gallery contains abstract paintings, usually from the cubism era, but not only! Enjoy!" },
                new Models.Gallery { Name = "Rembrandt Harmenszoon van Rijn", Description = "This gallery is dedicated to our beloved Rembrandt! Feel free to add as many paintings as possible!" }
            );
            context.SaveChanges();

            context.Painters.AddOrUpdate(
                new Models.Painter { Name = "Leonardo da Vinci", Description = "He was an Italian Renaissance polymath whose areas of interest included invention, painting, sculpting, architecture, science, music, mathematics, engineering, literature, anatomy, geology, astronomy, botany, writing, history, and cartography. Leonardo was, and is, renowned primarily as a painter. Among his works, the Mona Lisa is the most famous and most parodied portrait and The Last Supper the most reproduced religious painting of all time.", Country = "Italy", Link = "/Images/leonardo_da_vinci.jpg", BirthDate = DateTime.Parse("1452-04-15"), PassDate = DateTime.Parse("1519-05-02") },
                new Models.Painter { Name = "Oscar-Claude Monet", Description = "He was a founder of French Impressionist painting, and the most consistent and prolific practitioner of the movement's philosophy of expressing one's perceptions before nature, especially as applied to plein-air landscape painting. Monet's ambition of documenting the French countryside led him to adopt a method of painting the same scene many times in order to capture the changing of light and the passing of the seasons. From 1883 Monet lived in Giverny, where he began a vast landscaping project which included lily ponds that would become the subjects of his best-known works.", Country = "France", Link = "/Images/claude_monet.jpg", BirthDate = DateTime.Parse("1840-11-14"), PassDate = DateTime.Parse("1926-12-05") },
                new Models.Painter { Name = "Edvard Munch", Description = "He was a Norwegian painter and printmaker whose intensely evocative treatment of psychological themes built upon some of the main tenets of late 19th-century Symbolism and greatly influenced German Expressionism in the early 20th century. One of his best known works is The Scream of 1893.", Country = "Norway", Link = "/Images/edvard_munch.jpg", BirthDate = DateTime.Parse("1863-12-12"), PassDate = DateTime.Parse("1944-01-23") },
                new Models.Painter { Name = "Maurits Escher", Description = "He was a Dutch graphic artist who made mathematically inspired woodcuts, lithographs, and mezzotints. His work features mathematical objects and operations including impossible objects, explorations of infinity, reflection, symmetry, perspective, truncated and stellated polyhedra, hyperbolic geometry, and tessellations.  Early in his career, he drew inspiration from nature, making studies of insects, landscapes, and plants such as lichens, all of which he used as details in his artworks.", Country = "Netherlands", Link = "/Images/escher.jpg", BirthDate = DateTime.Parse("1898-06-17"), PassDate = DateTime.Parse("1972-03-27") },
                new Models.Painter { Name = "Marc Chagall", Description = "He was a Russian-French artist of Belarusian Jewish origin. An early modernist, he was associated with several major artistic styles and created works in virtually every artistic format, including painting, book illustrations, stained glass, stage sets, ceramic, tapestries and fine art prints. He had two basic reputations: as a pioneer of modernism and as a major Jewish artist. He experienced modernism's golden age in Paris, where he synthesized the art forms of Cubism, Symbolism, and Fauvism, and the influence of Fauvism gave rise to Surrealism.", Country = "Belarus", Link = "/Images/marc_chagall.jpg", BirthDate = DateTime.Parse("1887-06-24"), PassDate = DateTime.Parse("1985-03-28") },
                new Models.Painter { Name = "Michelangelo Buonarroti", Description = "He  was an Italian sculptor, painter, architect, and poet of the High Renaissance born in the Republic of Florence, who exerted influence on the development of Western art. Considered to be the greatest living artist during his lifetime, he has since been described as one of the greatest artists of all time. Despite making few forays beyond the arts, his versatility in the disciplines he took up was of such a high order that he is often considered a contender for the title of the archetypal Renaissance man, along with his rival and fellow Florentine Medici client, Leonardo da Vinci.", Country = "Italy", Link = "/Images/michelangelo_buonarotti.jpg", BirthDate = DateTime.Parse("1475-03-06"), PassDate = DateTime.Parse("1564-02-18") },
                new Models.Painter { Name = "Pablo Picasso", Description = "He  was a Spanish painter, sculptor, printmaker, ceramicist, stage designer, poet and playwright who spent most of his adult life in France. Regarded as one of the most influential artists of the 20th century, he is known for co-founding the Cubist movement, the invention of constructed sculpture, the co-invention of collage, and for the wide variety of styles that he helped develop and explore. Among his most famous works are the proto-Cubist Les Demoiselles d'Avignon (1907), and Guernica (1937), a dramatic portrayal of the bombing of Guernica by the German and Italian airforces.", Country = "Spain", Link = "/Images/pablo_picasso.jpg", BirthDate = DateTime.Parse("1881-10-25"), PassDate = DateTime.Parse("1973-04-08") },
                new Models.Painter { Name = "Rembrandt Harmenszoon van Rijn", Description = "He  was a Dutch draughtsman, painter, and printmaker. An innovative and prolific master in three media, he is generally considered one of the greatest visual artists in the history of art and the most important in Dutch art history. Unlike most Dutch masters of the 17th century, Rembrandt's works depict a wide range of style and subject matter, from portraits and self-portraits to landscapes, genre scenes, allegorical and historical scenes, biblical and mythological themes as well as animal studies.", Country = "Netherlands", Link = "/Images/rembrant.jpg", BirthDate = DateTime.Parse("1606-07-15"), PassDate = DateTime.Parse("1669-10-04") },
                new Models.Painter { Name = "Salvador Dalí", Description = "Dalí was a skilled draftsman, best known for the striking and bizarre images in his surrealist work. His expansive artistic repertoire included film, sculpture, and photography, in collaboration with a range of artists in a variety of media. Dalí was highly imaginative, and also enjoyed indulging in unusual and grandiose behavior. His eccentric manner and attention-grabbing public actions sometimes drew more attention than his artwork, to the dismay of those who held his work in high esteem, and to the irritation of his critics.", Country = "Spain", Link = "/Images/salvador_dali.jpg", BirthDate = DateTime.Parse("1904-05-11"), PassDate = DateTime.Parse("1989-01-23") },
                new Models.Painter { Name = "Vincent Willem van Gogh", Description = " He was a Dutch Post-Impressionist painter who is among the most famous and influential figures in the history of Western art. In just over a decade he created about 2,100 artworks, including around 860 oil paintings, most of them in the last two years of his life in France, where he died. They include landscapes, still lifes, portraits and self-portraits, and are characterised by bold colours and dramatic, impulsive and expressive brushwork that contributed to the foundations of modern art.", Country = "France", Link = "/Images/vincent_van_gogh.jpg", BirthDate = DateTime.Parse("1853-03-29"), PassDate = DateTime.Parse("1890-07-29") }
            );
            context.SaveChanges();
        }
    }
}
