import React from 'react';
import { ProjectName, StyledHeader, Nav, UnorderList, ListItem, StyledLink } from '../styles/Header.styled';

const Header = () => {
  return (
  
      <StyledHeader>
        <ProjectName>
          Elementary School
        </ProjectName>
        <Nav>
          <UnorderList>
          <ListItem>
            <StyledLink to={'/'}>Home</StyledLink>
          </ListItem>
            <ListItem>
            <StyledLink to={'/teacherList'}>Teachers</StyledLink>
            </ListItem>
            <ListItem>
              <StyledLink to={'/studentList'}>Students</StyledLink>
            </ListItem>
          </UnorderList>
        
        </Nav>
      </StyledHeader>
  )
}

export default Header