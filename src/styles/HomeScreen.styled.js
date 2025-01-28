import styled from 'styled-components';

export const HomeContainer = styled.div`
    display:flex;
    flex-direction:column;
    align-items:center;
    text-align:center;
    padding:20px;
`

export const HomeImage = styled.img`
    width:80%;
    height:85%;
    border-radius:10px;
    max-height:400px;
`

export const DescriptionTxt = styled.p`
    font-size:18px;
    color:#333,
    max-width:700px;
    margin-top: 20px;
    font-family:sans-serif;
`

export const GradeLevelsTitle = styled.h2`
    font-size:2rem;
    font-weight:bold;
    text-align:center;
    color:#2c3e50;
    margin-bottom:20px;
    padding:10px;
`

export const GradeCardContainer = styled.div`
    display:flex;
    justify-content:center;
    flex-wrap:wrap;
    gap: 20px;
    margin:0 auto;

`

export const GradeCard = styled.div`
    background-color:#f9f9f9;
    border:1px solid #ddd;
    border-radius:5px;
    text-align:center;
    padding:20px;
    margin:15px;
`

export const UpcomingEventsTitle = styled.h1`
    font-size:2rem;
    font-weight:bold;
    text-align:center;
    color:#2c3e50;
    margin-bottom:20px;
    padding:10px;

`

export const EventCardContainer = styled.div`
    display:flex;
    justify-content:center;
    flex-wrap:wrap;
    gap: 20px;
    margin:0 auto;
`

export const EventCard = styled.div`
    background-color:#f9f9f9;
    border:1px solid #ddd;
    border-radius:5px;
    text-align:center;
    padding:20px;
    margin:15px;
`

export const LatestNewsTitle = styled.h1`
    font-size:2rem;
    font-weight:bold;
    text-align:center;
    color:#2c3e50;
    margin-bottom:20px;
    padding:10px;
`
export const LatestNewsContainer = styled.div`
    display:flex;
    justify-content:center;
    flex-wrap:wrap;
    gap: 20px;
    margin:0 auto;
`
export const LatestNewsCard = styled.div`
    background-color:#f9f9f9;
    border:1px solid #ddd;
    border-radius:5px;
    text-align:center;
    padding:20px;
    margin:15px;
`

export const Footer = styled.footer`
    background-color: #f8f8f8;
    color: white;
    position:relative;
    bottom:0;
    width:100%;
    text-align:center;
    margin-top:20px;
`

export const SocialLinks = styled.div`
    margin-bottom:10px;

    a{
        color:#333;
        text-decoration:none;
        margin:0 15px;
        font-size:1.5rem;
    }

`

