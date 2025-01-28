import { styled } from 'styled-components';

export const FooterStyled = styled.footer`
    background-color: #f8f8f8;
    padding: 20px;
    text-align:center;
    display:flex;
    justify-content:center;
    align-items:center;
    flex-direction: column;
    width:100%;
    box-sizing:border-box;
    // min-height:60px;
    // position:fixed;
    // bottom:0;
    // left:0;
    // right:0;

    margin-top:auto;
    
`;

export const SocialLinks = styled.div`
    margin-bottom:10px;

    a{
        color: black;
        text-decoration:none;
        margin: 0 15px;
        font-size:1rem;
    }

    a:hover{
     color: #3498db;
    }
`;

export const CopyRight = styled.p`
    font-size:1rem;
    color:black;
    margin-top:10px;

`;