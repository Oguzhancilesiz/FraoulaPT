import React, { useState } from 'react';
import {
  Container,
  Typography,
  Grid,
  Box,
  TextField,
  Button,
  Card,
  CardContent,
  IconButton,
  Paper,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Snackbar,
  Alert
} from '@mui/material';
import {
  Email,
  Phone,
  LocationOn,
  Instagram,
  YouTube,
  WhatsApp,
  Send,
  TwoWheeler,
  FitnessCenter,
  AccessTime,
  Star
} from '@mui/icons-material';

const Contact = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phone: '',
    subject: '',
    message: '',
    messageType: 'general'
  });
  
  const [snackbar, setSnackbar] = useState({
    open: false,
    message: '',
    severity: 'success' as 'success' | 'error'
  });

  const messageTypes = [
    { value: 'general', label: 'Genel Bilgi' },
    { value: 'program', label: 'Program Danışmanlığı' },
    { value: 'product', label: 'Ürün Sorgusu' },
    { value: 'partnership', label: 'İş Birliği' },
    { value: 'support', label: 'Teknik Destek' }
  ];

  const contactInfo = [
    {
      icon: <Email sx={{ fontSize: 30, color: '#FF6B35' }} />,
      title: 'E-posta',
      info: 'info@fraoula.com',
      description: '24 saat içinde yanıt'
    },
    {
      icon: <Phone sx={{ fontSize: 30, color: '#FF6B35' }} />,
      title: 'Telefon',
      info: '+90 555 123 45 67',
      description: 'Hafta içi 09:00-18:00'
    },
    {
      icon: <WhatsApp sx={{ fontSize: 30, color: '#FF6B35' }} />,
      title: 'WhatsApp',
      info: '+90 555 123 45 67',
      description: 'Hızlı destek için'
    },
    {
      icon: <LocationOn sx={{ fontSize: 30, color: '#FF6B35' }} />,
      title: 'Adres',
      info: 'İstanbul, Türkiye',
      description: 'Randevu ile görüşme'
    }
  ];

  const socialLinks = [
    {
      icon: <Instagram sx={{ fontSize: 24 }} />,
      label: 'Instagram',
      url: '@fraoula_fitness',
      color: '#E4405F',
      followers: '125K'
    },
    {
      icon: <YouTube sx={{ fontSize: 24 }} />,
      label: 'YouTube',
              url: 'Fraoula Fitness',
      color: '#FF0000',
      followers: '89K'
    },
    {
      icon: <TwoWheeler sx={{ fontSize: 24 }} />,
      label: 'Fitness Blog',
      url: 'fraoula-fitness.com',
      color: '#FF6B35',
      followers: '45K'
    }
  ];

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Form gönderme işlemi
    setSnackbar({
      open: true,
      message: 'Mesajınız başarıyla gönderildi! 24 saat içinde size dönüş yapacağız.',
      severity: 'success'
    });
    
    // Form sıfırlama
    setFormData({
      name: '',
      email: '',
      phone: '',
      subject: '',
      message: '',
      messageType: 'general'
    });
  };

  const handleInputChange = (field: string, value: string) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };

  return (
    <Box sx={{ bgcolor: '#F8F9FA', minHeight: '100vh' }}>
      {/* Header Section */}
      <Box sx={{
        background: 'linear-gradient(135deg, #FF6B35 0%, #F7931E 100%)',
        py: 8,
        color: 'white'
      }}>
        <Container maxWidth="lg">
          <Box sx={{ textAlign: 'center' }}>
            <Typography variant="h2" sx={{ fontWeight: 700, mb: 2 }}>
              📞 İletişim
            </Typography>
            <Typography variant="h5" sx={{ opacity: 0.9, maxWidth: 600, mx: 'auto' }}>
              Fraoula ile iletişime geç! Fitness yolculuğunda yanındayız.
            </Typography>
          </Box>
        </Container>
      </Box>

      <Container maxWidth="xl" sx={{ py: 8 }}>
        <Grid container spacing={6}>
          {/* Contact Form */}
          <Grid xs={12} lg={8}>
            <Paper sx={{ p: 4, borderRadius: 3, boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)' }}>
              <Typography variant="h4" sx={{ fontWeight: 700, color: '#FF6B35', mb: 4 }}>
                Mesaj Gönder ✉️
              </Typography>
              
              <form onSubmit={handleSubmit}>
                <Grid container spacing={3}>
                  <Grid xs={12} md={6}>
                    <TextField
                      fullWidth
                      label="Adınız Soyadınız"
                      value={formData.name}
                      onChange={(e) => handleInputChange('name', e.target.value)}
                      required
                      sx={{
                        '& .MuiOutlinedInput-root': {
                          '&.Mui-focused fieldset': {
                            borderColor: '#FF6B35'
                          }
                        },
                        '& .MuiInputLabel-root.Mui-focused': {
                          color: '#FF6B35'
                        }
                      }}
                    />
                  </Grid>
                  
                  <Grid xs={12} md={6}>
                    <TextField
                      fullWidth
                      label="E-posta Adresiniz"
                      type="email"
                      value={formData.email}
                      onChange={(e) => handleInputChange('email', e.target.value)}
                      required
                      sx={{
                        '& .MuiOutlinedInput-root': {
                          '&.Mui-focused fieldset': {
                            borderColor: '#FF6B35'
                          }
                        },
                        '& .MuiInputLabel-root.Mui-focused': {
                          color: '#FF6B35'
                        }
                      }}
                    />
                  </Grid>
                  
                  <Grid xs={12} md={6}>
                    <TextField
                      fullWidth
                      label="Telefon Numaranız"
                      value={formData.phone}
                      onChange={(e) => handleInputChange('phone', e.target.value)}
                      sx={{
                        '& .MuiOutlinedInput-root': {
                          '&.Mui-focused fieldset': {
                            borderColor: '#FF6B35'
                          }
                        },
                        '& .MuiInputLabel-root.Mui-focused': {
                          color: '#FF6B35'
                        }
                      }}
                    />
                  </Grid>
                  
                  <Grid xs={12} md={6}>
                    <FormControl fullWidth>
                      <InputLabel>Mesaj Konusu</InputLabel>
                      <Select
                        value={formData.messageType}
                        onChange={(e) => handleInputChange('messageType', e.target.value)}
                        label="Mesaj Konusu"
                        sx={{
                          '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
                            borderColor: '#FF6B35'
                          }
                        }}
                      >
                        {messageTypes.map((type) => (
                          <MenuItem key={type.value} value={type.value}>
                            {type.label}
                          </MenuItem>
                        ))}
                      </Select>
                    </FormControl>
                  </Grid>
                  
                  <Grid xs={12}>
                    <TextField
                      fullWidth
                      label="Konu"
                      value={formData.subject}
                      onChange={(e) => handleInputChange('subject', e.target.value)}
                      required
                      sx={{
                        '& .MuiOutlinedInput-root': {
                          '&.Mui-focused fieldset': {
                            borderColor: '#FF6B35'
                          }
                        },
                        '& .MuiInputLabel-root.Mui-focused': {
                          color: '#FF6B35'
                        }
                      }}
                    />
                  </Grid>
                  
                  <Grid xs={12}>
                    <TextField
                      fullWidth
                      label="Mesajınız"
                      multiline
                      rows={6}
                      value={formData.message}
                      onChange={(e) => handleInputChange('message', e.target.value)}
                      required
                      placeholder="Mesajınızı buraya yazın..."
                      sx={{
                        '& .MuiOutlinedInput-root': {
                          '&.Mui-focused fieldset': {
                            borderColor: '#FF6B35'
                          }
                        },
                        '& .MuiInputLabel-root.Mui-focused': {
                          color: '#FF6B35'
                        }
                      }}
                    />
                  </Grid>
                  
                  <Grid xs={12}>
                    <Button
                      type="submit"
                      variant="contained"
                      size="large"
                      startIcon={<Send />}
                      sx={{
                        bgcolor: '#FF6B35',
                        py: 2,
                        px: 4,
                        fontSize: '1.1rem',
                        fontWeight: 600,
                        borderRadius: '30px',
                        '&:hover': {
                          bgcolor: '#E55A2B',
                          transform: 'translateY(-2px)'
                        },
                        transition: 'all 0.3s ease'
                      }}
                    >
                      Mesajı Gönder
                    </Button>
                  </Grid>
                </Grid>
              </form>
            </Paper>
          </Grid>

          {/* Contact Info & Social */}
          <Grid xs={12} lg={4}>
            <Box sx={{ position: 'sticky', top: 100 }}>
              {/* Contact Info */}
              <Paper sx={{ p: 4, borderRadius: 3, mb: 4, boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)' }}>
                <Typography variant="h5" sx={{ fontWeight: 700, color: '#FF6B35', mb: 3 }}>
                  İletişim Bilgileri 📞
                </Typography>
                
                {contactInfo.map((info, index) => (
                  <Box key={index} sx={{ mb: 3, display: 'flex', alignItems: 'center' }}>
                    <Box sx={{
                      bgcolor: '#FFF8F0',
                      borderRadius: '50%',
                      width: 60,
                      height: 60,
                      display: 'flex',
                      alignItems: 'center',
                      justifyContent: 'center',
                      mr: 3
                    }}>
                      {info.icon}
                    </Box>
                    <Box>
                      <Typography variant="h6" sx={{ fontWeight: 600, mb: 0.5 }}>
                        {info.title}
                      </Typography>
                      <Typography variant="body1" sx={{ color: '#FF6B35', fontWeight: 600 }}>
                        {info.info}
                      </Typography>
                      <Typography variant="caption" color="text.secondary">
                        {info.description}
                      </Typography>
                    </Box>
                  </Box>
                ))}
              </Paper>

              {/* Social Media */}
              <Paper sx={{ p: 4, borderRadius: 3, mb: 4, boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)' }}>
                <Typography variant="h5" sx={{ fontWeight: 700, color: '#FF6B35', mb: 3 }}>
                  Sosyal Medya 📱
                </Typography>
                
                {socialLinks.map((social, index) => (
                  <Box key={index} sx={{
                    mb: 2,
                    p: 2,
                    borderRadius: 2,
                    border: '2px solid #F5F5F5',
                    transition: 'all 0.3s ease',
                    '&:hover': {
                      borderColor: social.color,
                      transform: 'translateX(5px)'
                    }
                  }}>
                    <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
                      <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        <IconButton sx={{ color: social.color, mr: 2 }}>
                          {social.icon}
                        </IconButton>
                        <Box>
                          <Typography variant="subtitle2" sx={{ fontWeight: 600 }}>
                            {social.label}
                          </Typography>
                          <Typography variant="caption" color="text.secondary">
                            {social.url}
                          </Typography>
                        </Box>
                      </Box>
                      <Box sx={{ textAlign: 'right' }}>
                        <Typography variant="caption" sx={{ fontWeight: 600, color: social.color }}>
                          {social.followers}
                        </Typography>
                        <Typography variant="caption" sx={{ display: 'block' }} color="text.secondary">
                          takipçi
                        </Typography>
                      </Box>
                    </Box>
                  </Box>
                ))}
              </Paper>

              {/* Quick Stats */}
              <Paper sx={{ p: 4, borderRadius: 3, boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)' }}>
                <Typography variant="h5" sx={{ fontWeight: 700, color: '#FF6B35', mb: 3 }}>
                  Hızlı Bilgiler ⚡
                </Typography>
                
                <Box sx={{ mb: 3 }}>
                  <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                    <AccessTime sx={{ color: '#FF6B35', mr: 1 }} />
                    <Typography variant="subtitle2" sx={{ fontWeight: 600 }}>
                      Yanıt Süresi
                    </Typography>
                  </Box>
                  <Typography variant="body2" color="text.secondary">
                    Ortalama 2-4 saat içinde yanıtlıyoruz
                  </Typography>
                </Box>

                <Box sx={{ mb: 3 }}>
                  <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                    <Star sx={{ color: '#FFD700', mr: 1 }} />
                    <Typography variant="subtitle2" sx={{ fontWeight: 600 }}>
                      Müşteri Memnuniyeti
                    </Typography>
                  </Box>
                  <Typography variant="body2" color="text.secondary">
                    %98 memnuniyet oranı
                  </Typography>
                </Box>

                <Box sx={{ mb: 3 }}>
                  <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                    <FitnessCenter sx={{ color: '#FF6B35', mr: 1 }} />
                    <Typography variant="subtitle2" sx={{ fontWeight: 600 }}>
                      Deneyim
                    </Typography>
                  </Box>
                  <Typography variant="body2" color="text.secondary">
                    8+ yıl fitness koçluğu
                  </Typography>
                </Box>
              </Paper>
            </Box>
          </Grid>
        </Grid>
      </Container>

      {/* Snackbar */}
      <Snackbar
        open={snackbar.open}
        autoHideDuration={6000}
        onClose={() => setSnackbar(prev => ({ ...prev, open: false }))}
        anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
        <Alert
          onClose={() => setSnackbar(prev => ({ ...prev, open: false }))}
          severity={snackbar.severity}
          sx={{ width: '100%' }}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>
    </Box>
  );
};

export default Contact;
