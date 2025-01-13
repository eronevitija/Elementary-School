import { styled } from 'styled-components';
import { Link } from 'react-router-dom'

export const ProjectName = styled.div`
    font-size:24px;
    font-weight:bold;
    padding-left:20px;
    color:#333;
    
`;

export const StyledHeader = styled.header`
    background-color: #f8f8f8;
    padding:10px;
    color:white;
    display:flex;
    justify-content:space-between;
    align-items:center;
`;

export const Nav = styled.nav`
    display:flex;
    justify-content:center;
`;

export const UnorderList = styled.ul`
    display:flex;
    gap: 20px;
    list-style-type:none;
`;

export const ListItem = styled.li``;

export const StyledLink = styled(Link)`
    color: #333;
    text-decoration:none;
    font-size:16px;
    cursor:pointer;
   

    &:hover{
        color:#007bff;
    }
`;
