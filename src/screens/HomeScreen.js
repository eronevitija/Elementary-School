import React from 'react';
import { HomeContainer, 
         HomeImage, 
         DescriptionTxt,
         GradeLevelsTitle, 
         GradeCardContainer, 
         GradeCard,
         EventCardContainer,
         UpcomingEventsTitle,
         EventCard,
         LatestNewsTitle,
         LatestNewsContainer,
         LatestNewsCard,
   
        } from '../styles/HomeScreen.styled';

import HomeImageSrc from '../assets/7250404_31092.jpg';
import ContactUs from '../components/ContactUs';
import Footer from '../components/Footer';

const HomeScreen = () => {
  return (
    <HomeContainer>
      <DescriptionTxt>
        Ignite your passion for learning, and build a bright future.
      </DescriptionTxt>
      <HomeImage src={HomeImageSrc} alt='Elementary-School'/>

    <div>
      <GradeLevelsTitle>Grade Levels</GradeLevelsTitle>
      <GradeCardContainer>
        <GradeCard>
          <h3>Kindergarten</h3>
        </GradeCard>

        <GradeCard>
          <h3>First Grade</h3>
        </GradeCard>

        <GradeCard>
          <h3>Second Grade</h3>
        </GradeCard>

        <GradeCard>
          <h3>Third Grade</h3>
        </GradeCard>

        <GradeCard>
          <h3>Fifth Grade</h3>
        </GradeCard>
      </GradeCardContainer>
    </div>


    <div>
    <UpcomingEventsTitle>Upcoming Events</UpcomingEventsTitle>
     <EventCardContainer>
      <EventCard>
        <h3>Parent-Teacher Meeting</h3>
        <p>Join us to discuss your child's progress.</p>
        <span>January 23,2025</span>
      </EventCard>

      <EventCard>
        <h3>Winter Sports Event</h3>
        <p>Join us for a Winter Ski Trip.</p>
        <span>February 1,2025</span>
      </EventCard>

      <EventCard>
        <h3>Winter Sports Event</h3>
        <p>Join us for a Winter Ski Trip.</p>
        <span>February 1,2025</span>
      </EventCard>
     </EventCardContainer>
    </div>


    <div>
    <LatestNewsTitle>Latest News</LatestNewsTitle>
      <LatestNewsContainer>
        <LatestNewsCard>
          <h3>New Teacher Announced</h3>
            <p>We're thrilled to welcome Mr. Doe as the new teacher at our school! We can't wait for everyone to meet him.</p>
        </LatestNewsCard>
        <LatestNewsCard>
          <h3>New Books comming to the Library soon</h3>
            <p>Check them out soon!</p>
        </LatestNewsCard>
      </LatestNewsContainer>
    </div>

    <div>
      <ContactUs />
    </div>

      {/* <Footer /> */}

    </HomeContainer>
  )
}

export default HomeScreen