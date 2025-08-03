import React from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Box,
  Container,
  Typography,
  Button,
  Grid,
  Card,
  CardContent,
  IconButton,
  useTheme,
  useMediaQuery
} from '@mui/material';
import {
  PlayArrow,
  FitnessCenter,
  TwoWheeler,
  Star,
  Timeline,
  PersonalVideo,
  Store,
  EmojiEvents
} from '@mui/icons-material';

const HeroBanner = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));

  const features = [
    {
      icon: <FitnessCenter sx={{ fontSize: 40, color: '#FF6B35' }} />,
      title: 'Profesyonel Antrenman',
      description: 'Kişisel fitness koçluğu ve özel programlar'
    },
    {
      icon: <TwoWheeler sx={{ fontSize: 40, color: '#FF6B35' }} />,
      title: 'Fitness Yaşam Tarzı',
      description: 'Fitness tutkusu ve sağlıklı yaşam arasında mükemmel denge'
    },
    {
      icon: <PersonalVideo sx={{ fontSize: 40, color: '#FF6B35' }} />,
      title: 'Video Programları',
      description: 'Detaylı video rehberli antrenman programları'
    },
    {
      icon: <Store sx={{ fontSize: 40, color: '#FF6B35' }} />,
      title: 'Özel Ürünler',
      description: 'Seçilmiş fitness aksesuarları'
    }
  ];

  const stats = [
    { number: '1000+', label: 'Mutlu Müşteri' },
    { number: '50+', label: 'Video Program' },
    { number: '200+', label: 'Özel Ürün' },
    { number: '5★', label: 'Müşteri Puanı' }
  ];

  return (
    <Box sx={{ 
      background: 'linear-gradient(135deg, #FF6B35 0%, #F7931E 50%, #FF6B35 100%)',
      minHeight: '100vh',
      position: 'relative',
      overflow: 'hidden'
    }}>
      {/* Background Pattern */}
      <Box sx={{
        position: 'absolute',
        top: 0,
        left: 0,
        right: 0,
        bottom: 0,
        backgroundImage: `url("data:image/svg+xml,%3Csvg width='60' height='60' viewBox='0 0 60 60' xmlns='http://www.w3.org/2000/svg'%3E%3Cg fill='none' fill-rule='evenodd'%3E%3Cg fill='%23ffffff' fill-opacity='0.1'%3E%3Ccircle cx='30' cy='30' r='2'/%3E%3C/g%3E%3C/g%3E%3C/svg%3E")`,
        opacity: 0.3
      }} />

      <Container maxWidth="xl" sx={{ position: 'relative', zIndex: 2, pt: 8 }}>
        <Grid container spacing={4} alignItems="center" sx={{ minHeight: '80vh' }}>
          {/* Sol Taraf - Hero Content */}
          <Grid xs={12} lg={6}>
            <Box sx={{ color: 'white', textAlign: isMobile ? 'center' : 'left' }}>
              {/* Badge */}
              <Box sx={{
                display: 'inline-flex',
                alignItems: 'center',
                bgcolor: 'rgba(255,255,255,0.2)',
                borderRadius: '25px',
                px: 3,
                py: 1,
                mb: 3,
                backdropFilter: 'blur(10px)'
              }}>
                <EmojiEvents sx={{ color: '#FFD700', mr: 1 }} />
                <Typography variant="body2" sx={{ fontWeight: 600 }}>
                  #1 Fitness Influencer
                </Typography>
              </Box>

              {/* Ana Başlık */}
              <Typography 
                variant="h1" 
                sx={{ 
                  fontSize: { xs: '2.5rem', md: '3.5rem', lg: '4rem' },
                  fontWeight: 900,
                  lineHeight: 1.1,
                  mb: 3,
                  textShadow: '2px 2px 4px rgba(0,0,0,0.3)'
                }}
              >
                FRAOULA
                                  <Box component="span" sx={{ display: 'block', color: '#FFD700' }}>
                    FITNESS
                  </Box>
              </Typography>

              {/* Alt Başlık */}
              <Typography 
                variant="h5" 
                sx={{ 
                  mb: 4, 
                  opacity: 0.95,
                  fontWeight: 400,
                  maxWidth: 500
                }}
              >
                Turuncu saçlı fitness tutkunun dünyasına hoş geldin! 
                Profesyonel antrenman programları ve seçkin ürünlerle hedeflerine ulaş.
              </Typography>

              {/* CTA Buttons */}
              <Box sx={{ 
                display: 'flex', 
                gap: 2, 
                flexDirection: isMobile ? 'column' : 'row',
                alignItems: isMobile ? 'center' : 'flex-start'
              }}>
                <Button
                  variant="contained"
                  size="large"
                  startIcon={<PlayArrow />}
                  onClick={() => navigate('/video-programs')}
                  sx={{
                    bgcolor: 'white',
                    color: '#FF6B35',
                    px: 4,
                    py: 2,
                    fontSize: '1.1rem',
                    fontWeight: 700,
                    borderRadius: '30px',
                    boxShadow: '0 8px 25px rgba(0,0,0,0.2)',
                    '&:hover': {
                      bgcolor: '#FFD700',
                      color: '#FF6B35',
                      transform: 'translateY(-2px)',
                      boxShadow: '0 12px 35px rgba(0,0,0,0.3)'
                    },
                    transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)'
                  }}
                >
                  Spor Programları
                </Button>
                
                <Button
                  variant="outlined"
                  size="large"
                  startIcon={<Store />}
                  onClick={() => navigate('/marketplace')}
                  sx={{
                    borderColor: 'white',
                    color: 'white',
                    px: 4,
                    py: 2,
                    fontSize: '1.1rem',
                    fontWeight: 700,
                    borderRadius: '30px',
                    borderWidth: 2,
                    '&:hover': {
                      borderColor: '#FFD700',
                      color: '#FFD700',
                      bgcolor: 'rgba(255, 215, 0, 0.1)',
                      borderWidth: 2
                    }
                  }}
                >
                  Ürünleri Keşfet
                </Button>
              </Box>

              {/* Stats */}
              <Grid container spacing={3} sx={{ mt: 6 }}>
                {stats.map((stat, index) => (
                  <Grid xs={6} md={3} key={index}>
                    <Box sx={{ textAlign: 'center' }}>
                      <Typography variant="h4" sx={{ fontWeight: 900, mb: 0.5 }}>
                        {stat.number}
                      </Typography>
                      <Typography variant="body2" sx={{ opacity: 0.8 }}>
                        {stat.label}
                      </Typography>
                    </Box>
                  </Grid>
                ))}
              </Grid>
            </Box>
          </Grid>

          {/* Sağ Taraf - Hero Image/Video */}
          <Grid xs={12} lg={6}>
            <Box sx={{ 
              position: 'relative',
              textAlign: 'center',
              mt: isMobile ? 4 : 0
            }}>
              {/* Ana Görsel Container */}
              <Box sx={{
                position: 'relative',
                borderRadius: '20px',
                overflow: 'hidden',
                boxShadow: '0 20px 60px rgba(0,0,0,0.3)',
                background: 'linear-gradient(45deg, rgba(255,255,255,0.1) 0%, rgba(255,255,255,0.05) 100%)',
                backdropFilter: 'blur(10px)',
                border: '1px solid rgba(255,255,255,0.2)'
              }}>
                {/* Video Placeholder */}
                <Box sx={{
                  height: 400,
                  background: 'linear-gradient(45deg, rgba(0,0,0,0.3) 0%, rgba(0,0,0,0.1) 100%)',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  position: 'relative'
                }}>
                  <IconButton sx={{
                    bgcolor: 'rgba(255,255,255,0.9)',
                    color: '#FF6B35',
                    width: 80,
                    height: 80,
                    '&:hover': {
                      bgcolor: 'white',
                      transform: 'scale(1.1)'
                    },
                    transition: 'all 0.3s ease'
                  }}>
                    <PlayArrow sx={{ fontSize: 40 }} />
                  </IconButton>
                  
                  {/* Video Label */}
                  <Box sx={{
                    position: 'absolute',
                    bottom: 20,
                    left: 20,
                    bgcolor: 'rgba(0,0,0,0.7)',
                    color: 'white',
                    px: 3,
                    py: 1,
                    borderRadius: '20px',
                    backdropFilter: 'blur(10px)'
                  }}>
                    <Typography variant="body2" sx={{ fontWeight: 600 }}>
                      🔥 Antrenman Tanıtım Videosu
                    </Typography>
                  </Box>
                </Box>
              </Box>

              {/* Floating Elements */}
              <Box sx={{
                position: 'absolute',
                top: -20,
                right: -20,
                bgcolor: '#FFD700',
                color: '#FF6B35',
                borderRadius: '50%',
                width: 60,
                height: 60,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                fontWeight: 900,
                fontSize: '1.2rem',
                boxShadow: '0 8px 25px rgba(255, 215, 0, 0.4)',
                animation: 'float 3s ease-in-out infinite'
              }}>
                ⭐
              </Box>
            </Box>
          </Grid>
        </Grid>

        {/* Features Section */}
        <Box sx={{ py: 8 }}>
          <Typography 
            variant="h3" 
            sx={{ 
              textAlign: 'center', 
              color: 'white', 
              mb: 6,
              fontWeight: 700
            }}
          >
            Neden Fraoula?
          </Typography>
          
          <Grid container spacing={4}>
            {features.map((feature, index) => (
              <Grid xs={12} sm={6} md={3} key={index}>
                <Card sx={{
                  height: '100%',
                  background: 'rgba(255,255,255,0.1)',
                  backdropFilter: 'blur(10px)',
                  border: '1px solid rgba(255,255,255,0.2)',
                  borderRadius: 3,
                  transition: 'all 0.3s ease',
                  '&:hover': {
                    transform: 'translateY(-10px)',
                    background: 'rgba(255,255,255,0.15)',
                    boxShadow: '0 20px 40px rgba(0,0,0,0.2)'
                  }
                }}>
                  <CardContent sx={{ textAlign: 'center', p: 4 }}>
                    <Box sx={{ mb: 2 }}>
                      {feature.icon}
                    </Box>
                    <Typography variant="h6" sx={{ color: 'white', mb: 2, fontWeight: 600 }}>
                      {feature.title}
                    </Typography>
                    <Typography variant="body2" sx={{ color: 'rgba(255,255,255,0.8)' }}>
                      {feature.description}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Box>
      </Container>

      {/* CSS Animations */}
      <style>
        {`
          @keyframes float {
            0%, 100% { transform: translateY(0px); }
            50% { transform: translateY(-20px); }
          }
        `}
      </style>
    </Box>
  );
};

export default HeroBanner;
