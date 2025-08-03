import React, { useState } from 'react';
import {
  Container,
  Grid,
  Card,
  CardMedia,
  CardContent,
  Typography,
  Button,
  Box,
  Chip,
  Rating,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Paper
} from '@mui/material';
import {
  PlayArrow,
  FitnessCenter,
  Schedule,
  Star,
  Favorite,
  Share,
  PlayCircleOutline,
  Lock,
  CheckCircle,
  Search,
  FilterList
} from '@mui/icons-material';

const VideoPrograms = () => {
  const [programs, setPrograms] = useState([
    {
      id: "1",
      title: "30 Günlük Fitness Programı",
      description: "Yeni başlayanlar için detaylı video rehberi ile 30 günlük kapsamlı fitness programı.",
      price: 299.99,
      thumbnailUrl: "/images/programs/fitness30.jpg",
      category: "Fitness",
      difficulty: "Beginner",
      durationMinutes: 45,
      isFeatured: true,
      isInfluencerChoice: true,

      rating: 4.9,
      reviewCount: 247,
      instructor: "Fraoula PT",
      videoCount: 30,
      totalHours: 22.5,
      isPurchased: false,
      hasPreview: true
    },
    {
      id: "2", 
      title: "Yoga & Mindfulness",
      description: "Zihin ve beden dengesini sağlayan 21 günlük yoga ve meditasyon programı.",
      price: 199.99,
      thumbnailUrl: "/images/programs/yoga21.jpg",
      category: "Yoga",
      difficulty: "Intermediate",
      durationMinutes: 35,
      isFeatured: true,
      isInfluencerChoice: false,
      rating: 4.8,
      reviewCount: 189,
      instructor: "Melis Yoga",
      videoCount: 21,
      totalHours: 12.25,
      isPurchased: true,
      hasPreview: true
    },
    {
      id: "3",
      title: "HIIT Cardio Master",
      description: "Yoğun kardiyovasküler antrenman ile hızlı sonuç alma programı.",
      price: 249.99,
      thumbnailUrl: "/images/programs/hiit.jpg",
      category: "Cardio",
      difficulty: "Advanced",
      durationMinutes: 25,
      isFeatured: false,
      isInfluencerChoice: true,

      rating: 4.7,
      reviewCount: 156,
      instructor: "Can HIIT",
      videoCount: 16,
      totalHours: 6.75,
      isPurchased: false,
      hasPreview: true
    }
  ]);

  const [selectedProgram, setSelectedProgram] = useState<any>(null);
  const [previewOpen, setPreviewOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState('');
  const [selectedDifficulty, setSelectedDifficulty] = useState('');

  const categories = ['Fitness', 'Yoga', 'Pilates', 'Cardio', 'Strength', 'Dance'];
  const difficulties = ['Beginner', 'Intermediate', 'Advanced', 'Expert'];

  const getDifficultyColor = (difficulty: string) => {
    switch (difficulty) {
      case 'Beginner': return '#4CAF50';
      case 'Intermediate': return '#FF9800';
      case 'Advanced': return '#F44336';
      case 'Expert': return '#9C27B0';
      default: return '#9E9E9E';
    }
  };

  const getCategoryIcon = (category: string) => {
    switch (category) {
      case 'Fitness': return <FitnessCenter />;
      case 'Yoga': return <PlayArrow />;
      case 'Cardio': return <Schedule />;
      default: return <PlayArrow />;
    }
  };

  const handlePurchase = (program: any) => {
    // Satın alma işlemi
    alert(`${program.title} programı ₺${program.price} karşılığında satın alındı!`);
    setPrograms(programs.map(p => 
      p.id === program.id ? { ...p, isPurchased: true } : p
    ));
  };

  const handlePreview = (program: any) => {
    setSelectedProgram(program);
    setPreviewOpen(true);
  };

  const filteredPrograms = programs.filter(program => {
    const matchesSearch = program.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         program.description.toLowerCase().includes(searchTerm.toLowerCase());
    const matchesCategory = selectedCategory === '' || program.category === selectedCategory;
    const matchesDifficulty = selectedDifficulty === '' || program.difficulty === selectedDifficulty;
    
    return matchesSearch && matchesCategory && matchesDifficulty;
  });

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      {/* Header */}
      <Box sx={{ mb: 4, textAlign: 'center' }}>
        <Typography variant="h3" sx={{ fontWeight: 700, color: '#FF6B35', mb: 2 }}>
          🎥 Video Programları
        </Typography>
        <Typography variant="h6" color="text.secondary">
          Uzman eğitmenlerden profesyonel video programları
        </Typography>
      </Box>

      {/* Filters */}
      <Paper sx={{ p: 3, mb: 4, borderRadius: 3 }}>
        <Grid container spacing={3} alignItems="center">
          <Grid xs={12} md={4}>
            <TextField
              fullWidth
              label="Program Ara"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              InputProps={{
                startAdornment: <Search sx={{ mr: 1, color: '#FF6B35' }} />
              }}
            />
          </Grid>
          <Grid xs={12} md={3}>
            <FormControl fullWidth>
              <InputLabel>Kategori</InputLabel>
              <Select
                value={selectedCategory}
                onChange={(e) => setSelectedCategory(e.target.value)}
                label="Kategori"
              >
                <MenuItem value="">Tümü</MenuItem>
                {categories.map((category) => (
                  <MenuItem key={category} value={category}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      {getCategoryIcon(category)}
                      <Typography sx={{ ml: 1 }}>{category}</Typography>
                    </Box>
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Grid>
          <Grid xs={12} md={3}>
            <FormControl fullWidth>
              <InputLabel>Zorluk</InputLabel>
              <Select
                value={selectedDifficulty}
                onChange={(e) => setSelectedDifficulty(e.target.value)}
                label="Zorluk"
              >
                <MenuItem value="">Tümü</MenuItem>
                {difficulties.map((difficulty) => (
                  <MenuItem key={difficulty} value={difficulty}>
                    <Chip 
                      label={difficulty} 
                      size="small"
                      sx={{ bgcolor: getDifficultyColor(difficulty), color: 'white' }}
                    />
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </Grid>
          <Grid xs={12} md={2}>
            <Button
              fullWidth
              variant="outlined"
              startIcon={<FilterList />}
              sx={{ borderColor: '#FF6B35', color: '#FF6B35', height: 56 }}
            >
              Filtrele
            </Button>
          </Grid>
        </Grid>
      </Paper>

      {/* Program Cards */}
      <Grid container spacing={4}>
        {filteredPrograms.map((program) => (
          <Grid xs={12} md={6} lg={4} key={program.id}>
            <Card sx={{
              borderRadius: 3,
              overflow: 'hidden',
              boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)',
              transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
              position: 'relative',
              '&:hover': {
                transform: 'translateY(-8px)',
                boxShadow: '0 16px 48px rgba(255, 107, 53, 0.2)'
              }
            }}>
              {/* Badges */}
              <Box sx={{ position: 'absolute', top: 16, left: 16, zIndex: 2 }}>
                {program.isFeatured && (
                  <Chip 
                    label="Öne Çıkan" 
                    size="small" 
                    sx={{ bgcolor: '#FF6B35', color: 'white', mb: 1, display: 'block' }}
                  />
                )}
                {program.isInfluencerChoice && (
                  <Chip 
                    label="⭐ Influencer Seçimi" 
                    size="small" 
                    sx={{ bgcolor: '#FFA500', color: 'white' }}
                  />
                )}
              </Box>

              {/* Purchase Status */}
              {program.isPurchased && (
                <Box sx={{ position: 'absolute', top: 16, right: 16, zIndex: 2 }}>
                  <Chip 
                    icon={<CheckCircle />}
                    label="Satın Alındı" 
                    size="small" 
                    sx={{ bgcolor: '#4CAF50', color: 'white' }}
                  />
                </Box>
              )}

              {/* Thumbnail */}
              <Box sx={{ position: 'relative' }}>
                <CardMedia
                  component="img"
                  height="200"
                  image={program.thumbnailUrl || '/images/programs/default.jpg'}
                  alt={program.title}
                />
                <Box sx={{
                  position: 'absolute',
                  top: 0,
                  left: 0,
                  right: 0,
                  bottom: 0,
                  bgcolor: 'rgba(0,0,0,0.3)',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  opacity: 0,
                  transition: 'opacity 0.3s',
                  '&:hover': { opacity: 1 }
                }}>
                  <IconButton
                    sx={{ 
                      bgcolor: 'rgba(255,255,255,0.9)', 
                      color: '#FF6B35',
                      '&:hover': { bgcolor: 'white' }
                    }}
                    onClick={() => handlePreview(program)}
                  >
                    <PlayCircleOutline sx={{ fontSize: 48 }} />
                  </IconButton>
                </Box>
              </Box>

              <CardContent>
                {/* Title & Category */}
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'start', mb: 1 }}>
                  <Typography variant="h6" sx={{ fontWeight: 600, flex: 1 }}>
                    {program.title}
                  </Typography>
                  <Chip 
                    label={program.category}
                    size="small"
                    icon={getCategoryIcon(program.category)}
                    sx={{ ml: 1 }}
                  />
                </Box>

                {/* Instructor */}
                <Typography variant="body2" color="text.secondary" sx={{ mb: 1 }}>
                  Eğitmen: {program.instructor}
                </Typography>

                {/* Description */}
                <Typography variant="body2" color="text.secondary" sx={{ mb: 2, height: 40, overflow: 'hidden' }}>
                  {program.description}
                </Typography>

                {/* Program Info */}
                <Box sx={{ display: 'flex', gap: 2, mb: 2, flexWrap: 'wrap' }}>
                  <Box sx={{ display: 'flex', alignItems: 'center' }}>
                    <PlayArrow sx={{ fontSize: 16, color: '#FF6B35', mr: 0.5 }} />
                    <Typography variant="caption">{program.videoCount} Video</Typography>
                  </Box>
                  <Box sx={{ display: 'flex', alignItems: 'center' }}>
                    <Schedule sx={{ fontSize: 16, color: '#FF6B35', mr: 0.5 }} />
                    <Typography variant="caption">{program.totalHours}h</Typography>
                  </Box>
                  <Chip 
                    label={program.difficulty}
                    size="small"
                    sx={{ 
                      bgcolor: getDifficultyColor(program.difficulty), 
                      color: 'white',
                      fontSize: '0.75rem'
                    }}
                  />
                </Box>

                {/* Rating */}
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <Rating value={program.rating} readOnly size="small" />
                  <Typography variant="body2" sx={{ ml: 1 }}>
                    {program.rating} ({program.reviewCount} değerlendirme)
                  </Typography>
                </Box>

                {/* Influencer Comment */}


                {/* Price & Actions */}
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <Typography variant="h6" sx={{ fontWeight: 700, color: '#FF6B35' }}>
                    ₺{program.price}
                  </Typography>
                  
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <IconButton size="small" sx={{ color: '#E91E63' }}>
                      <Favorite />
                    </IconButton>
                    <IconButton size="small" sx={{ color: '#2196F3' }}>
                      <Share />
                    </IconButton>
                    {program.isPurchased ? (
                      <Button
                        variant="contained"
                        size="small"
                        startIcon={<PlayArrow />}
                        sx={{ bgcolor: '#4CAF50', '&:hover': { bgcolor: '#45A049' } }}
                      >
                        İzle
                      </Button>
                    ) : (
                      <Button
                        variant="contained"
                        size="small"
                        startIcon={<PlayArrow />}
                        onClick={() => handlePurchase(program)}
                        sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
                      >
                        Satın Al
                      </Button>
                    )}
                  </Box>
                </Box>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      {/* Preview Dialog */}
      <Dialog 
        open={previewOpen} 
        onClose={() => setPreviewOpen(false)}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          {selectedProgram?.title} - Önizleme
        </DialogTitle>
        <DialogContent sx={{ p: 0 }}>
          {selectedProgram && (
            <Box>
              {/* Video Player Placeholder */}
              <Box sx={{ 
                height: 300, 
                bgcolor: '#000', 
                display: 'flex', 
                alignItems: 'center', 
                justifyContent: 'center',
                position: 'relative'
              }}>
                <PlayCircleOutline sx={{ fontSize: 80, color: 'white', opacity: 0.7 }} />
                <Typography variant="h6" sx={{ 
                  position: 'absolute', 
                  bottom: 16, 
                  left: 16, 
                  color: 'white' 
                }}>
                  Önizleme Video
                </Typography>
              </Box>

              {/* Program Details */}
              <Box sx={{ p: 3 }}>
                <Typography variant="h6" gutterBottom>
                  Program İçeriği
                </Typography>
                <List>
                  <ListItem>
                    <ListItemIcon>
                      <PlayArrow sx={{ color: '#FF6B35' }} />
                    </ListItemIcon>
                    <ListItemText 
                      primary="Giriş ve Isınma Hareketleri" 
                      secondary="5 dakika - Ücretsiz önizleme"
                    />
                  </ListItem>
                  <ListItem>
                    <ListItemIcon>
                      <Lock sx={{ color: '#9E9E9E' }} />
                    </ListItemIcon>
                    <ListItemText 
                      primary="Ana Antrenman Serileri" 
                      secondary="35 dakika - Satın alma gerekli"
                    />
                  </ListItem>
                  <ListItem>
                    <ListItemIcon>
                      <Lock sx={{ color: '#9E9E9E' }} />
                    </ListItemIcon>
                    <ListItemText 
                      primary="Soğuma ve Esneme" 
                      secondary="5 dakika - Satın alma gerekli"
                    />
                  </ListItem>
                </List>
              </Box>
            </Box>
          )}
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setPreviewOpen(false)}>
            Kapat
          </Button>
          {selectedProgram && !selectedProgram.isPurchased && (
            <Button 
              variant="contained"
              onClick={() => {
                handlePurchase(selectedProgram);
                setPreviewOpen(false);
              }}
              sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
            >
              ₺{selectedProgram.price} - Satın Al
            </Button>
          )}
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default VideoPrograms;
