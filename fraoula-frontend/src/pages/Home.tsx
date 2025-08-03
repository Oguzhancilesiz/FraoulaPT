import React from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Box,
  Container,
  Typography,
  Grid,
  Card,
  CardMedia,
  CardContent,
  Button,
  Chip,
  Rating,
  IconButton,
  Paper
} from '@mui/material';
import {
  PlayArrow,
  Store,
  Star,
  FitnessCenter,
  TwoWheeler,
  ArrowForward,
  Favorite
} from '@mui/icons-material';
import HeroBanner from '../components/HeroBanner';

const Home = () => {
  const featuredPrograms = [
    {
      id: 1,
      title: "Güç & Fitness Kombo",
      description: "Sporculara özel tasarlanmış kuvvet antrenmanı",
      price: 299.99,
      thumbnail: "/images/programs/motor-fitness.jpg",
      rating: 4.9,
      reviews: 127,
      duration: "8 hafta",
      category: "Özel Program"
    },
    {
      id: 2,
      title: "Turuncu Güç Serisi", 
      description: "Fraoula'nın kişisel favori antrenman serisi",
      price: 199.99,
      thumbnail: "/images/programs/orange-power.jpg",
      rating: 4.8,
      reviews: 89,
      duration: "4 hafta",
      category: "Power Training"
    },
    {
      id: 3,
      title: "Başlangıç Rehberi",
      description: "Fitness yolculuğuna başlayanlar için temel program",
      price: 149.99,
      thumbnail: "/images/programs/beginner.jpg", 
      rating: 4.7,
      reviews: 203,
      duration: "6 hafta",
      category: "Beginner"
    }
  ];

  const featuredProducts = [
    {
      id: 1,
      name: "Fraoula Signature Tişört",
      price: 199.99,
      image: "/images/products/signature-tee.jpg",
      rating: 4.9,
      isNew: true
    },
    {
      id: 2,
      name: "Fitness Eldivenleri Pro",
      price: 299.99,
      image: "/images/products/motor-gloves.jpg",
      rating: 4.8,
      isNew: false
    },
    {
      id: 3,
      name: "Turuncu Protein Powder",
      price: 249.99,
      image: "/images/products/protein.jpg",
      rating: 4.7,
      isNew: true
    },
    {
      id: 4,
      name: "Fitness Tracker Band",
      price: 399.99,
      image: "/images/products/tracker.jpg",
      rating: 4.6,
      isNew: false
    }
  ];

  const testimonials = [
    {
      name: "Ahmet K.",
      comment: "Fraoula'nın programları sayesinde hem kondisyonum arttı hem de spor salonunda daha güçlü hissediyorum!",
      rating: 5,
      image: "/avatars/ahmet.jpg"
    },
    {
      name: "Zeynep M.",
      comment: "Turuncu güç serisi harika! Videoları çok net anlatılmış, evde rahatlıkla takip edebiliyorum.",
      rating: 5,
      image: "/avatars/zeynep.jpg"
    },
    {
      name: "Can D.",
      comment: "Fitness kombinasyonu mükemmel. Artık uzun antrenmanlarda yorulmuyorum!",
      rating: 5,
      image: "/avatars/can.jpg"
    }
  ];

  return (
    <Box>
      {/* Hero Banner */}
      <HeroBanner />

      {/* Featured Programs Section */}
      <Container maxWidth="xl" sx={{ py: 8 }}>
        <Box sx={{ textAlign: 'center', mb: 6 }}>
          <Typography variant="h3" sx={{ fontWeight: 700, color: '#FF6B35', mb: 2 }}>
            🎥 Öne Çıkan Programlar
          </Typography>
          <Typography variant="h6" color="text.secondary">
            Fraoula'nın özel tasarladığı fitness programları
          </Typography>
        </Box>

        <Grid container spacing={4}>
          {featuredPrograms.map((program) => (
            <Grid xs={12} md={4} key={program.id}>
              <Card sx={{
                borderRadius: 3,
                overflow: 'hidden',
                boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)',
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
                '&:hover': {
                  transform: 'translateY(-8px)',
                  boxShadow: '0 16px 48px rgba(255, 107, 53, 0.2)'
                }
              }}>
                <Box sx={{ position: 'relative' }}>
                  <CardMedia
                    component="img"
                    height="200"
                    image={program.thumbnail}
                    alt={program.title}
                  />
                  <IconButton sx={{
                    position: 'absolute',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    bgcolor: 'rgba(255, 107, 53, 0.9)',
                    color: 'white',
                    '&:hover': { bgcolor: '#FF6B35' }
                  }}>
                    <PlayArrow sx={{ fontSize: 32 }} />
                  </IconButton>
                  <Chip 
                    label={program.category}
                    size="small"
                    sx={{
                      position: 'absolute',
                      top: 16,
                      left: 16,
                      bgcolor: '#FF6B35',
                      color: 'white'
                    }}
                  />
                </Box>
                
                <CardContent>
                  <Typography variant="h6" sx={{ fontWeight: 600, mb: 1 }}>
                    {program.title}
                  </Typography>
                  <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
                    {program.description}
                  </Typography>
                  
                  <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                    <Rating value={program.rating} readOnly size="small" />
                    <Typography variant="caption" sx={{ ml: 1 }}>
                      {program.rating} ({program.reviews} değerlendirme)
                    </Typography>
                  </Box>

                  <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <Typography variant="h6" sx={{ fontWeight: 700, color: '#FF6B35' }}>
                      ₺{program.price}
                    </Typography>
                    <Button
                      variant="contained"
                      size="small"
                      sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
                    >
                      İncele
                    </Button>
                  </Box>
                </CardContent>
              </Card>
            </Grid>
          ))}
        </Grid>

        <Box sx={{ textAlign: 'center', mt: 4 }}>
          <Button
            variant="outlined"
            size="large"
            endIcon={<ArrowForward />}
            sx={{ borderColor: '#FF6B35', color: '#FF6B35' }}
          >
            Tüm Programları Gör
          </Button>
        </Box>
      </Container>

      {/* Featured Products Section */}
      <Box sx={{ bgcolor: '#F8F9FA', py: 8 }}>
        <Container maxWidth="xl">
          <Box sx={{ textAlign: 'center', mb: 6 }}>
            <Typography variant="h3" sx={{ fontWeight: 700, color: '#FF6B35', mb: 2 }}>
              🛍️ Özel Ürünler
            </Typography>
            <Typography variant="h6" color="text.secondary">
              Seçilmiş fitness aksesuarları
            </Typography>
          </Box>

          <Grid container spacing={3}>
            {featuredProducts.map((product) => (
              <Grid xs={12} sm={6} md={3} key={product.id}>
                <Card sx={{
                  borderRadius: 3,
                  overflow: 'hidden',
                  position: 'relative',
                  transition: 'all 0.3s ease',
                  '&:hover': {
                    transform: 'translateY(-5px)',
                    boxShadow: '0 12px 32px rgba(255, 107, 53, 0.15)'
                  }
                }}>
                  {product.isNew && (
                    <Chip 
                      label="YENİ"
                      size="small"
                      sx={{
                        position: 'absolute',
                        top: 12,
                        right: 12,
                        bgcolor: '#FFD700',
                        color: '#FF6B35',
                        fontWeight: 700,
                        zIndex: 2
                      }}
                    />
                  )}
                  
                  <CardMedia
                    component="img"
                    height="200"
                    image={product.image}
                    alt={product.name}
                  />
                  
                  <CardContent>
                    <Typography variant="h6" sx={{ fontWeight: 600, mb: 1 }}>
                      {product.name}
                    </Typography>
                    
                    <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                      <Rating value={product.rating} readOnly size="small" />
                      <Typography variant="caption" sx={{ ml: 1 }}>
                        {product.rating}
                      </Typography>
                    </Box>

                    <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                      <Typography variant="h6" sx={{ fontWeight: 700, color: '#FF6B35' }}>
                        ₺{product.price}
                      </Typography>
                      <IconButton size="small" sx={{ color: '#E91E63' }}>
                        <Favorite />
                      </IconButton>
                    </Box>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>

          <Box sx={{ textAlign: 'center', mt: 4 }}>
            <Button
              variant="contained"
              size="large"
              startIcon={<Store />}
              sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
            >
              Mağazayı Keşfet
            </Button>
          </Box>
        </Container>
      </Box>

      {/* Testimonials Section */}
      <Container maxWidth="xl" sx={{ py: 8 }}>
        <Box sx={{ textAlign: 'center', mb: 6 }}>
          <Typography variant="h3" sx={{ fontWeight: 700, color: '#FF6B35', mb: 2 }}>
            💬 Müşteri Yorumları
          </Typography>
          <Typography variant="h6" color="text.secondary">
            Fraoula ailesinden gelen geri bildirimler
          </Typography>
        </Box>

        <Grid container spacing={4}>
          {testimonials.map((testimonial, index) => (
            <Grid xs={12} md={4} key={index}>
              <Paper sx={{
                p: 4,
                borderRadius: 3,
                height: '100%',
                border: '2px solid #FFF8F0',
                transition: 'all 0.3s ease',
                '&:hover': {
                  borderColor: '#FF6B35',
                  transform: 'translateY(-5px)'
                }
              }}>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 3 }}>
                  <Box
                    component="img"
                    src={testimonial.image}
                    alt={testimonial.name}
                    sx={{
                      width: 50,
                      height: 50,
                      borderRadius: '50%',
                      mr: 2,
                      border: '2px solid #FF6B35'
                    }}
                  />
                  <Box>
                    <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
                      {testimonial.name}
                    </Typography>
                    <Rating value={testimonial.rating} readOnly size="small" />
                  </Box>
                </Box>
                
                <Typography variant="body1" sx={{ fontStyle: 'italic', color: 'text.secondary' }}>
                  "{testimonial.comment}"
                </Typography>
              </Paper>
            </Grid>
          ))}
        </Grid>
      </Container>

      {/* CTA Section */}
      <Box sx={{
        background: 'linear-gradient(135deg, #FF6B35 0%, #F7931E 100%)',
        py: 8
      }}>
        <Container maxWidth="md">
          <Box sx={{ textAlign: 'center', color: 'white' }}>
            <Typography variant="h3" sx={{ fontWeight: 700, mb: 3 }}>
              Fraoula Ailesine Katıl! 🔥
            </Typography>
            <Typography variant="h6" sx={{ mb: 4, opacity: 0.9 }}>
              Fitness hedeflerine ulaşmak için gerekli her şey burada.
              Hemen başla ve farkı hisset!
            </Typography>
            <Button
              variant="contained"
              size="large"
              sx={{
                bgcolor: 'white',
                color: '#FF6B35',
                px: 6,
                py: 2,
                fontSize: '1.2rem',
                fontWeight: 700,
                borderRadius: '30px',
                '&:hover': {
                  bgcolor: '#FFD700',
                  transform: 'translateY(-2px)'
                }
              }}
            >
              Hemen Başla
            </Button>
          </Box>
        </Container>
      </Box>
    </Box>
  );
};

export default Home;
