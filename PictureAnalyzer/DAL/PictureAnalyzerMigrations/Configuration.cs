﻿namespace PictureAnalyzer.DAL.PictureAnalyzerMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PictureAnalyzer.DAL.PictureAnalyzerDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\PictureAnalyzerMigrations";
        }

        protected override void Seed(PictureAnalyzer.DAL.PictureAnalyzerDb context)
        {
            context.Profiles.AddOrUpdate(
                p => p.Name,
                new Models.Profile { Name = "Type A Personality", Description = "Type A individuals tend to be very competitive and self-critical. They strive toward goals without feeling a sense of joy in their efforts or accomplishments. Interrelated with this is the presence of a significant life imbalance.This is characterized by a high work involvement.Type A individuals are easily ‘wound up’ and tend to overreact. Type A people seem to be in a constant struggle against the clock. Often, they try to do more than one thing at a time. Type A individuals tend to be easily aroused to anger or hostility and see the worse in others, displaying anger, envy and a lack of compassion. When this behavior is expressed overtly (i.e.physical behavior) it generally involves aggression and possible bullying." },
                new Models.Profile { Name = "Type B Personality", Description = "Type B personality, by definition, are noted to live at lower stress levels. They typically work steadily, and may enjoy achievement, although they have a greater tendency to disregard physical or mental stress when they do not achieve. People with Type B personality tend to be more tolerant of others, are more relaxed than Type A individuals, more reflective, experience lower levels of anxiety and display higher level of imagination and creativity. When faced with competition, they may focus less on winning or losing and more on enjoying the game regardless of winning or losing. Type B individuals are sometimes attracted to careers of creativity. Their personal character may enjoy exploring ideas and concepts. They are often reflective, and think of the outer and inner world." },
                new Models.Profile { Name = "Type C Personality", Description = "A type C personality individual is not particularly assertive, so they don’t speak up to tell others what they like or not and they may support the needs of others, ignoring their own. Over the long-term, this can bring about stress which can lead to depression. They have a love for detail and are well focused.  They love to figure out how things really work. Type C personality individuals tend to take life seriously and this makes them excellent workers, however this can become extreme to the point of perfectionism.  If they go to this extreme, they will find it difficult to work with others and spend a far amount of time working alone and becoming introverted..  However,  their passion for detail give them excellent analytical skills meaning they are very intelligent and dependable." },
                new Models.Profile { Name = "Type D Personality", Description = "Type D personality, a concept used in the field of medical psychology, is defined as the joint tendency towards negative affectivity (e.g. worry, irritability, gloom) and social inhibition (e.g. reticence and a lack of self-assurance). The letter D stands for distressed. Individuals with a Type D personality have the tendency to experience increased negative emotions across time and situations and tend not to share these emotions with others, because of fear of rejection or disapproval. Type D personalities usually have a low self esteem and a great fear of disapproval and this is the primary reason that prevents them from opening up to others. A small event that is usually overlooked by others can bother a type D a lot and even ruin his mood." }
                );
            context.SaveChanges();

            context.Colors.AddOrUpdate(
                c => c.Name,
                new Models.Color { Name = "Red", Description = "The color red is a warm and positive color associated with our most physical needs and our will to survive. It exudes a strong and powerful masculine energy. Red is energizing. It excites the emotions and motivates us to take action. It signifies a pioneering spirit and leadership qualities, promoting ambition and determination.It is also strong-willed and can give confidence to those who are shy or lacking in will power. Being the color of physical movement, the color red awakens our physical life force.", PersonalityTraits = "Having red as a favorite color identifies people as extroverted and optimistic, courageous and confident. They are action oriented and physically active, having strong survival instincts. They like to be the center of attention. They are ambitious and competitive and like to be the winner - they are achievement orientated and second place is not good enough for them. With them it is all or nothing." },
                new Models.Color { Name = "Orange", Description = "The color orange radiates warmth and happiness, combining the physical energy and stimulation of red with the cheerfulness of yellow. Orange relates to gut reaction as opposed to the physical reaction of red or the mental reaction of yellow. Orange offers emotional strength in difficult times. The color psychology of orange is optimistic and uplifting, rejuvenating our spirit. In fact orange is so optimistic and uplifting that we should all find ways to use it in our everyday life.", PersonalityTraits = "﻿With orange as their favorite color, they are warm, optimistic, extroverted and often flamboyant. They are friendly, good-natured and generally agreeable persons. They are assertive and determined rather than aggressive - having a personality color orange means they are more light-hearted and less intense than those who love red. They thrive on human social contact and social gatherings, bringing all types together.vWhile they are charming and sociable they do tend to be a show-off." },
                new Models.Color { Name = "Yellow", Description = "The color yellow relates to acquired knowledge. It is the color which resonates with the left or logic side of the brain stimulating our mental faculties and creating mental agility and perception. Being the lightest hue of the spectrum, the color psychology of yellow is uplifting and illuminating, offering hope, happiness, cheerfulness and fun. In the meaning of colors, yellow inspires original thought and inquisitiveness.", PersonalityTraits = "They have a happy disposition and are cheerful and fun to be with. They are creative, often being the ones who come up with new ideas - an idea person who needs others to bring the ideas into reality - they tend to have their head in the clouds much of the time. With a personality color yellow,they can be very critical of themselves as well as others - they are perfectionists.They analyze everything, all the time, and are methodical in their thinking.They are impulsive and make quick decisions, but, out of anxiety,jump in too quickly and rush things." },
                new Models.Color { Name = "Green", Description = "The color green relates to balance and harmony. From a color psychology perspective, it is the great balancer of the heart and the emotions, creating equilibrium between the head and the heart. From a meaning of colors perspective, green is also the color of growth, the color of spring, of renewal and rebirth. It renews and restores depleted energy. It is the sanctuary away from the stresses of modern living, restoring us back to a sense of well being. This is why there is so much of this relaxing color on the earth, and why we need to keep it that way.", PersonalityTraits = "They are a practical, down-to-earth persons with a love of nature. They are stable and well balanced or are striving for balance - in seeking this balance, they can at times become unsettled and anxious. Having a personality color green means they are kind, generous and compassionate - good to have around during a crisis as they remain calm and take control of the situation until it is resolved. They  are caring and nurturing to others - however they must be careful not to neglect their own needs while giving to others. They are intelligent  love to learn, quickly understanding new concepts." },
                new Models.Color { Name = "Blue", Description = "This color is one of trust, responsibility, honesty and loyalty. It is sincere, reserved and quiet, and doesn't like to make a fuss or draw attention. It hates confrontation, and likes to do things in its own way. From a color psychology perspective, blue is reliable and responsible. This color exhibits an inner security and confidence. You can rely on it to take control and do the right thing in difficult times.", PersonalityTraits = "People having the personality color blue means they are conservative, reliable and trustworthy - they are quite trusting of others although they are very wary in the beginning until they are sure of the other person. At the same time, they also have a deep need to be trusted. They are not impulsive or spontaneous - they always think before they speak and act and do everything at their own pace in their own time. They take time to process and share their feelings, being genuine and sincere." },
                new Models.Color { Name = "Indigo", Description = "The color indigo is the color of intuition and perception and is helpful in opening the third eye. It promotes deep concentration during times of introspection and meditation, helping you achieve deeper levels of consciousness. It relies on intuition rather than gut feeling. Indigo is a deep midnight blue. It is a combination of deep blue and violet and holds the attributes of both these colors. Powerful and dignified, indigo conveys integrity and deep sincerity.", PersonalityTraits = "With indigo as their favorite color, they are honest, compassionate and understanding. Integrity is extremely important to them. They need structure in their life - organization is important to them and they can be quite inflexible when it comes to order in their life. Having a personality color indigo, they love rituals and traditions. They look to the past when planning for the future. They are conscientious and reliable - a good person to have around in a crisis. Being a personality color indigo indicates they are hungry for the meaning of life." },
                new Models.Color { Name = "Turquoise", Description = "The color turquoise helps to open the lines of communication between the heart and the spoken word. It presents as a friendly and happy color enjoying life. In color psychology, turquoise controls and heals the emotions creating emotional balance and stability. In the process it can appear to be on an emotional roller coaster, up and down, until it balances itself. It radiates the peace, calm and tranquility of blue and the balance and growth of green with the uplifting energy of yellow.", PersonalityTraits = "If this is their favorite color, they are friendly and approachable, easy to communicate with. They are compassionate, empathetic and caring. They have a heightened sense of creativity and sensitivity. They speak from the heart and love sharing your inner most thoughts. As a personality color turquoise they usually have highly developed intuitive abilities. They seek spiritual fulfillment, and they are often an evolved or old soul." },
                new Models.Color { Name = "Pink", Description = "A combination of red and white, pink contains the need for action of red, helping it to achieve the potential for success and insight offered by white. It is the passion and power of red softened with the purity, openness and completeness of white. The deeper the pink, the more passion and energy it exhibits. Pink is feminine and romantic, affectionate and intimate, thoughtful and caring. It tones down the physical passion of red replacing it with a gentle loving energy.", PersonalityTraits = "They are loving, kind, generous and sensitive to the needs of others. They are friendly and approachable with a warmth and softness others are drawn to. They are the nurturers of the world. With a personality color pink, they have a maternal instinct, with a need to protect and take care of others. They also have a need for this caring to be reciprocated as they do tend to neglect themselves. They are very much in touch with their femininity - this includes men who are in touch with their feminine side. They are romantic, sensual and sensitive." },
                new Models.Color { Name = "Brown", Description = "The color brown is a serious, down-to-earth color signifying stability, structure and support. Relating to the protection and support of the family unit, with a keen sense of duty and responsibility, brown takes its obligations seriously. It encourages a strong need for security and a sense of belonging, with family and friends being of utmost importance. In the meaning of colors, brown is the color of material security and the accumulation of material possessions.", PersonalityTraits = "They are honest, down-to-earth and wholesome, people with both feet planted firmly on the ground. They are steady and reliable and quietly confident. They are friendly and approachable, genuine and sincere. They have a keen sense of duty and responsibility, taking their  obligations very seriously. Family and family life is extremely important to them. They like physical comfort, simplicity and quality. They are a loyal and trustworthy friends, supportive and dependable. They are sensitive to the needs of others and sensitive to criticism by others." },
                new Models.Color { Name = "Grey", Description = "The color gray is an unemotional color. It is detached, neutral, impartial and indecisive. From a color psychology perspective, gray is the color of compromise - being neither black nor white, it is the transition between two non-colors. The closer gray gets to black, the more dramatic and mysterious it becomes. The closer it gets to silver or white, the more illuminating and lively it becomes. Being both motionless and emotionless, gray is solid and stable, creating a sense of calm and composure, relief from a chaotic world.", PersonalityTraits = "They are neutral about life, often to the point of being indifferent. If they love gray, they are trying to protect themselves from the chaotic outside world, even to the point of isolating themselves from others, leaving them with the feeling that they don't really fit in or belong anywhere. They prefer a safe, secure and balanced existence and never desire much excitement. They will usually compromise in order to keep the balance and stability they so desperately seek. They are practical and calm, do not like to attract attention and are simply seeking a contented life." },
                new Models.Color { Name = "White", Description = "The color white is color at its most complete and pure, the color of perfection. The psychological meaning of white is purity, innocence, wholeness and completion. In color psychology white is the color of new beginnings, of wiping the slate clean, so to speak. It is the blank canvas waiting to be written upon. While white isn't stimulating to the senses, it opens the way for the creation of anything the mind can conceive.", PersonalityTraits = "They are neat and immaculate in their appearance, almost to the point of being fanatical. They have impeccable standards of cleanliness and hygiene and they expect others to adhere to their high standards. They are far-sighted, with a positive and optimistic nature. They are well-balanced, sensible, discreet and wise. With a personality color white, they are cautious, practical and careful with money. They think carefully before acting, being definitely not prone to impulsive behavior. They tend to have a great deal of self control." },
                new Models.Color { Name = "Black", Description = "The color black relates to the hidden, the secretive and the unknown, and as a result it creates an air of mystery. It keeps things bottled up inside, hidden from the world. In color psychology this color gives protection from external emotional stress. It creates a barrier between itself and the outside world, providing comfort while protecting its emotions and feelings, and hiding its vulnerabilities, insecurities and lack of self confidence. Black is the absorption of all color and the absence of light. Black hides, while white brings to light. What black covers, white uncovers.", PersonalityTraits = "Prestige and power are important to them. They are independent, strong-willed and determined and like to be in control of themselves and situations. As a lover of black they may be conservative and conventional - black is restricting and contained. With black as their personality color, they may be too serious for their own good. They may appear intimidating to even their closest colleagues and friends, with an authoritarian, demanding and dictatorial attitude." }
                );
            context.SaveChanges();

            context.Types.AddOrUpdate(
                new Models.Type { Name="Portrait", Description="No current description", PersonalityTraits="lonelyness"}
                );
        }
    }
}
