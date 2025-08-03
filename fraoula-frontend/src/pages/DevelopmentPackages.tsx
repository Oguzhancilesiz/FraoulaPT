import React, { useState } from 'react';
import {
  Container,
  Grid,
  Card,
  CardContent,
  CardMedia,
  Typography,
  Button,
  Box,
  Chip,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Paper,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Divider,
  Avatar,
  Rating,
  Badge,
  Switch,
  FormControlLabel
} from '@mui/material';
import {
  LocalOffer,
  Add,
  Schedule,
  VideoLibrary,
  FitnessCenter,
  Restaurant,
  Support,
  Telegram,
  Group,
  Star,
  TrendingUp,
  Edit,
  Delete,
  ShoppingCart,
  CheckCircle,
  PlayArrow,
  Assignment,
  People,
  Timer,
  AttachMoney
} from '@mui/icons-material';

const DevelopmentPackages = () => {
  const [packages, setPackages] = useState<any[]>([
    {
      id: "1",
      name: "Elite Fitness Transformation",
      description: "Sporcular için özel tasarlanmış 12 haftalık tam gelişim paketi. Güç, dayanıklılık ve koordinasyon.",
      price: 1499.99,
      discountPrice: 999.99,
      packageType: "Premium",
      durationDays: 84,
      imageUrl: "/images/packages/motor-transformation.jpg",
      isActive: true,
      isFeatured: true,
      targetGroup: "Elite Fitness Warriors",
      targetLevel: "Intermediate",
      primaryFocus: "Strength",
      videoCount: 48,
      workoutCount: 36,
      includesNutrition: true,
      includesPersonalSupport: true,
      includesTelegramAccess: true,
      telegramChannelLink: "https://t.me/elite_transformation",
      rating: 4.9,
      reviewCount: 127,
      soldCount: 89,
      activeSubscriptions: 76
    },
    {
      id: "2",
      name: "Başlangıç Rehberi",
      description: "Fitness dünyasına yeni adım atanlar için kapsamlı 6 haftalık temel eğitim paketi.",
      price: 599.99,
      discountPrice: null,
      packageType: "Basic",
      durationDays: 42,
      imageUrl: "/images/packages/beginner-guide.jpg",
      isActive: true,
      isFeatured: false,
      targetGroup: "Başlangıç Grubu",
      targetLevel: "Beginner",
      primaryFocus: "GeneralFitness",
      videoCount: 24,
      workoutCount: 18,
      includesNutrition: true,
      includesPersonalSupport: false,
      includesTelegramAccess: true,
      telegramChannelLink: "https://t.me/beginner_guide",
      rating: 4.8,
      reviewCount: 203,
      soldCount: 156,
      activeSubscriptions: 134
    },
    {
      id: "3",
      name: "Elite Performance Boost",
      description: "İleri seviye sporcular için 16 haftalık yoğun performans artırım programı.",
      price: 2499.99,
      discountPrice: 1999.99,
      packageType: "Elite",
      durationDays: 112,
      imageUrl: "/images/packages/elite-performance.jpg",
      isActive: true,
      isFeatured: true,
      targetGroup: "Elite Performance",
      targetLevel: "Advanced",
      primaryFocus: "Strength",
      videoCount: 64,
      workoutCount: 48,
      includesNutrition: true,
      includesPersonalSupport: true,
      includesTelegramAccess: true,
      telegramChannelLink: "https://t.me/elite_performance",
      rating: 5.0,
      reviewCount: 89,
      soldCount: 45,
      activeSubscriptions: 42
    }
  ]);

  const [packageDialogOpen, setPackageDialogOpen] = useState(false);
  const [selectedPackage, setSelectedPackage] = useState<any>(null);
  const [detailDialogOpen, setDetailDialogOpen] = useState(false);

  const [newPackageData, setNewPackageData] = useState({
    name: '',
    description: '',
    price: 0,
    packageType: 'Basic',
    durationDays: 30,
    targetLevel: 'Beginner',
    primaryFocus: 'GeneralFitness',
    videoCount: 0,
    workoutCount: 0,
    includesNutrition: false,
    includesPersonalSupport: false,
    includesTelegramAccess: true
  });

  const packageTypes = [
    { value: 'Basic', label: 'Temel', color: '#27AE60', price: '299-999₺' },
    { value: 'Premium', label: 'Premium', color: '#FF6B35', price: '999-1999₺' },
    { value: 'Elite', label: 'Elite', color: '#9B59B6', price: '1999₺+' }
  ];

  const focusAreas = [
    { value: 'WeightLoss', label: 'Kilo Verme', icon: <TrendingUp /> },
    { value: 'MuscleGain', label: 'Kas Kazanma', icon: <FitnessCenter /> },
    { value: 'Endurance', label: 'Dayanıklılık', icon: <Timer /> },
    { value: 'Strength', label: 'Güç', icon: <FitnessCenter /> },
    { value: 'GeneralFitness', label: 'Genel Fitness', icon: <Star /> }
  ];

  const getTypeInfo = (type: string) => {
    return packageTypes.find(t => t.value === type) || packageTypes[0];
  };

  const getFocusInfo = (focus: string) => {
    return focusAreas.find(f => f.value === focus) || focusAreas[0];
  };

  const handleCreatePackage = () => {
    const newPackage = {
      id: (Math.max(...packages.map(p => parseInt(p.id))) + 1).toString(),
      ...newPackageData,
      discountPrice: null,
      imageUrl: "/images/packages/default.jpg",
      isActive: true,
      isFeatured: false,
      targetGroup: null,
      telegramChannelLink: "",
      rating: 5.0,
      reviewCount: 0,
      soldCount: 0,
      activeSubscriptions: 0
    };

    setPackages([...packages, newPackage]);
    setPackageDialogOpen(false);
    setNewPackageData({
      name: '',
      description: '',
      price: 0,
      packageType: 'Basic',
      durationDays: 30,
      targetLevel: 'Beginner',
      primaryFocus: 'GeneralFitness',
      videoCount: 0,
      workoutCount: 0,
      includesNutrition: false,
      includesPersonalSupport: false,
      includesTelegramAccess: true
    });
  };

  const stats = [
    {
      title: 'Toplam Paket',
      value: packages.length,
      icon: <LocalOffer />,
      color: '#FF6B35'
    },
    {
      title: 'Aktif Abonelik',
      value: packages.reduce((sum, p) => sum + p.activeSubscriptions, 0),
      icon: <People />,
      color: '#27AE60'
    },
    {
      title: 'Toplam Satış',
      value: packages.reduce((sum, p) => sum + p.soldCount, 0),
      icon: <ShoppingCart />,
      color: '#F39C12'
    },
    {
      title: 'Ortalama Rating',
      value: (packages.reduce((sum, p) => sum + p.rating, 0) / packages.length).toFixed(1),
      icon: <Star />,
      color: '#9B59B6'
    }
  ];

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      {/* Header */}
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 4 }}>
        <Typography variant="h4" sx={{ fontWeight: 700, color: '#FF6B35' }}>
          📦 Gelişim Paketleri
        </Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setPackageDialogOpen(true)}
          sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
        >
          Yeni Paket Oluştur
        </Button>
      </Box>

      {/* Stats Cards */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        {stats.map((stat, index) => (
          <Grid xs={12} sm={6} md={3} key={index}>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <Avatar sx={{ bgcolor: stat.color, mr: 2 }}>
                    {stat.icon}
                  </Avatar>
                  <Box>
                    <Typography variant="h5">{stat.value}</Typography>
                    <Typography variant="body2" color="text.secondary">
                      {stat.title}
                    </Typography>
                  </Box>
                </Box>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      {/* Packages Grid */}
      <Grid container spacing={4}>
        {packages.map((pkg) => {
          const typeInfo = getTypeInfo(pkg.packageType);
          const focusInfo = getFocusInfo(pkg.primaryFocus);
          const hasDiscount = pkg.discountPrice && pkg.discountPrice < pkg.price;
          const discountPercentage = hasDiscount ? Math.round(((pkg.price - pkg.discountPrice!) / pkg.price) * 100) : 0;

          return (
            <Grid xs={12} md={6} lg={4} key={pkg.id}>
              <Card sx={{
                borderRadius: 3,
                overflow: 'hidden',
                boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)',
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
                position: 'relative',
                border: pkg.isFeatured ? '2px solid #FFD700' : 'none',
                '&:hover': {
                  transform: 'translateY(-8px)',
                  boxShadow: '0 16px 48px rgba(255, 107, 53, 0.2)'
                }
              }}>
                {/* Featured Badge */}
                {pkg.isFeatured && (
                  <Box sx={{
                    position: 'absolute',
                    top: -10,
                    left: 20,
                    bgcolor: '#FFD700',
                    color: '#FF6B35',
                    px: 2,
                    py: 0.5,
                    borderRadius: 2,
                    fontWeight: 700,
                    fontSize: '0.8rem',
                    zIndex: 2,
                    boxShadow: '0 4px 12px rgba(255, 215, 0, 0.3)'
                  }}>
                    ⭐ ÖNE ÇIKAN
                  </Box>
                )}

                {/* Package Image */}
                <Box sx={{ position: 'relative' }}>
                  <CardMedia
                    component="img"
                    height="200"
                    image={pkg.imageUrl}
                    alt={pkg.name}
                  />

                  {/* Package Type Badge */}
                  <Chip
                    label={typeInfo.label}
                    size="small"
                    sx={{
                      position: 'absolute',
                      top: 12,
                      left: 12,
                      bgcolor: typeInfo.color,
                      color: 'white',
                      fontWeight: 600
                    }}
                  />

                  {/* Discount Badge */}
                  {hasDiscount && (
                    <Box sx={{
                      position: 'absolute',
                      top: 12,
                      right: 12,
                      bgcolor: '#E74C3C',
                      color: 'white',
                      borderRadius: '50%',
                      width: 50,
                      height: 50,
                      display: 'flex',
                      alignItems: 'center',
                      justifyContent: 'center',
                      fontWeight: 700,
                      fontSize: '0.8rem'
                    }}>
                      %{discountPercentage}
                    </Box>
                  )}

                  {/* Duration & Rating */}
                  <Box sx={{
                    position: 'absolute',
                    bottom: 12,
                    left: 12,
                    right: 12,
                    display: 'flex',
                    justifyContent: 'space-between'
                  }}>
                    <Chip
                      icon={<Schedule />}
                      label={`${pkg.durationDays} gün`}
                      size="small"
                      sx={{
                        bgcolor: 'rgba(0,0,0,0.7)',
                        color: 'white'
                      }}
                    />
                    <Box sx={{
                      display: 'flex',
                      alignItems: 'center',
                      bgcolor: 'rgba(0,0,0,0.7)',
                      borderRadius: 2,
                      px: 1,
                      py: 0.5
                    }}>
                      <Star sx={{ fontSize: 16, color: '#FFD700', mr: 0.5 }} />
                      <Typography variant="caption" sx={{ color: 'white', fontWeight: 600 }}>
                        {pkg.rating}
                      </Typography>
                    </Box>
                  </Box>
                </Box>

                <CardContent>
                  {/* Package Name */}
                  <Typography variant="h6" sx={{ fontWeight: 600, mb: 1 }}>
                    {pkg.name}
                  </Typography>

                  {/* Target Group */}
                  {pkg.targetGroup && (
                    <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                      <Group sx={{ fontSize: 16, color: '#FF6B35', mr: 0.5 }} />
                      <Typography variant="caption" color="text.secondary">
                        {pkg.targetGroup}
                      </Typography>
                    </Box>
                  )}

                  {/* Description */}
                  <Typography variant="body2" color="text.secondary" sx={{ mb: 2, height: 48, overflow: 'hidden' }}>
                    {pkg.description}
                  </Typography>

                  {/* Package Contents */}
                  <Box sx={{ display: 'flex', gap: 1, mb: 2, flexWrap: 'wrap' }}>
                    <Chip 
                      icon={<VideoLibrary />}
                      label={`${pkg.videoCount} Video`}
                      size="small"
                      variant="outlined"
                    />
                    <Chip 
                      icon={<FitnessCenter />}
                      label={`${pkg.workoutCount} Antrenman`}
                      size="small"
                      variant="outlined"
                    />
                    {pkg.includesNutrition && (
                      <Chip 
                        icon={<Restaurant />}
                        label="Beslenme"
                        size="small"
                        color="success"
                        variant="outlined"
                      />
                    )}
                  </Box>

                  {/* Features */}
                  <Box sx={{ mb: 2 }}>
                    {pkg.includesPersonalSupport && (
                      <Box sx={{ display: 'flex', alignItems: 'center', mb: 0.5 }}>
                        <CheckCircle sx={{ fontSize: 16, color: '#27AE60', mr: 1 }} />
                        <Typography variant="caption">Kişisel Destek</Typography>
                      </Box>
                    )}
                    {pkg.includesTelegramAccess && (
                      <Box sx={{ display: 'flex', alignItems: 'center', mb: 0.5 }}>
                        <Telegram sx={{ fontSize: 16, color: '#0088CC', mr: 1 }} />
                        <Typography variant="caption">Telegram Grubu</Typography>
                      </Box>
                    )}
                  </Box>

                  {/* Stats */}
                  <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
                    <Typography variant="caption" color="text.secondary">
                      {pkg.soldCount} satış
                    </Typography>
                    <Typography variant="caption" color="text.secondary">
                      {pkg.activeSubscriptions} aktif
                    </Typography>
                    <Typography variant="caption" color="text.secondary">
                      {pkg.reviewCount} yorum
                    </Typography>
                  </Box>

                  {/* Price & Actions */}
                  <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <Box>
                      {hasDiscount ? (
                        <Box>
                          <Typography variant="h6" sx={{ fontWeight: 700, color: '#E74C3C' }}>
                            ₺{pkg.discountPrice}
                          </Typography>
                          <Typography variant="caption" sx={{ textDecoration: 'line-through', color: 'text.secondary' }}>
                            ₺{pkg.price}
                          </Typography>
                        </Box>
                      ) : (
                        <Typography variant="h6" sx={{ fontWeight: 700, color: '#FF6B35' }}>
                          ₺{pkg.price}
                        </Typography>
                      )}
                    </Box>

                    <Box sx={{ display: 'flex', gap: 1 }}>
                      <IconButton 
                        size="small" 
                        sx={{ color: '#27AE60' }}
                        onClick={() => {
                          setSelectedPackage(pkg);
                          setDetailDialogOpen(true);
                        }}
                      >
                        <PlayArrow />
                      </IconButton>
                      <IconButton size="small" sx={{ color: '#FF6B35' }}>
                        <Edit />
                      </IconButton>
                    </Box>
                  </Box>
                </CardContent>
              </Card>
            </Grid>
          );
        })}
      </Grid>

      {/* Create Package Dialog */}
      <Dialog open={packageDialogOpen} onClose={() => setPackageDialogOpen(false)} maxWidth="md" fullWidth>
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          Yeni Gelişim Paketi Oluştur
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          <Grid container spacing={3} sx={{ mt: 1 }}>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Paket Adı"
                value={newPackageData.name}
                onChange={(e) => setNewPackageData({...newPackageData, name: e.target.value})}
                required
              />
            </Grid>
            
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Açıklama"
                multiline
                rows={3}
                value={newPackageData.description}
                onChange={(e) => setNewPackageData({...newPackageData, description: e.target.value})}
              />
            </Grid>
            
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Fiyat (₺)"
                type="number"
                value={newPackageData.price}
                onChange={(e) => setNewPackageData({...newPackageData, price: parseFloat(e.target.value)})}
              />
            </Grid>
            
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Paket Türü</InputLabel>
                <Select
                  value={newPackageData.packageType}
                  onChange={(e) => setNewPackageData({...newPackageData, packageType: e.target.value})}
                  label="Paket Türü"
                >
                  {packageTypes.map((type) => (
                    <MenuItem key={type.value} value={type.value}>
                      <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', width: '100%' }}>
                        <Typography>{type.label}</Typography>
                        <Typography variant="caption" color="text.secondary">
                          {type.price}
                        </Typography>
                      </Box>
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Süre (gün)"
                type="number"
                value={newPackageData.durationDays}
                onChange={(e) => setNewPackageData({...newPackageData, durationDays: parseInt(e.target.value)})}
              />
            </Grid>
            
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Ana Odak</InputLabel>
                <Select
                  value={newPackageData.primaryFocus}
                  onChange={(e) => setNewPackageData({...newPackageData, primaryFocus: e.target.value})}
                  label="Ana Odak"
                >
                  {focusAreas.map((focus) => (
                    <MenuItem key={focus.value} value={focus.value}>
                      <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        {focus.icon}
                        <Typography sx={{ ml: 1 }}>{focus.label}</Typography>
                      </Box>
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Video Sayısı"
                type="number"
                value={newPackageData.videoCount}
                onChange={(e) => setNewPackageData({...newPackageData, videoCount: parseInt(e.target.value)})}
              />
            </Grid>
            
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Antrenman Sayısı"
                type="number"
                value={newPackageData.workoutCount}
                onChange={(e) => setNewPackageData({...newPackageData, workoutCount: parseInt(e.target.value)})}
              />
            </Grid>
            
            <Grid xs={12}>
              <Box sx={{ display: 'flex', gap: 2, flexWrap: 'wrap' }}>
                <FormControlLabel
                  control={
                    <Switch
                      checked={newPackageData.includesNutrition}
                      onChange={(e) => setNewPackageData({...newPackageData, includesNutrition: e.target.checked})}
                    />
                  }
                  label="Beslenme Planı"
                />
                <FormControlLabel
                  control={
                    <Switch
                      checked={newPackageData.includesPersonalSupport}
                      onChange={(e) => setNewPackageData({...newPackageData, includesPersonalSupport: e.target.checked})}
                    />
                  }
                  label="Kişisel Destek"
                />
                <FormControlLabel
                  control={
                    <Switch
                      checked={newPackageData.includesTelegramAccess}
                      onChange={(e) => setNewPackageData({...newPackageData, includesTelegramAccess: e.target.checked})}
                    />
                  }
                  label="Telegram Erişimi"
                />
              </Box>
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setPackageDialogOpen(false)}>İptal</Button>
          <Button 
            variant="contained"
            onClick={handleCreatePackage}
            sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
          >
            Paketi Oluştur
          </Button>
        </DialogActions>
      </Dialog>

      {/* Package Detail Dialog */}
      <Dialog open={detailDialogOpen} onClose={() => setDetailDialogOpen(false)} maxWidth="md" fullWidth>
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          {selectedPackage?.name} - Detaylar
        </DialogTitle>
        <DialogContent sx={{ p: 0 }}>
          {selectedPackage && (
            <Box>
              {/* Package Image */}
              <Box
                component="img"
                src={selectedPackage.imageUrl}
                alt={selectedPackage.name}
                sx={{ width: '100%', height: 200, objectFit: 'cover' }}
              />
              
              <Box sx={{ p: 3 }}>
                {/* Description */}
                <Typography variant="body1" sx={{ mb: 3 }}>
                  {selectedPackage.description}
                </Typography>

                {/* Package Contents */}
                <Typography variant="h6" sx={{ mb: 2, color: '#FF6B35' }}>
                  Paket İçeriği
                </Typography>
                <List>
                  <ListItem>
                    <ListItemIcon>
                      <VideoLibrary sx={{ color: '#FF6B35' }} />
                    </ListItemIcon>
                    <ListItemText primary={`${selectedPackage.videoCount} Eğitim Videosu`} />
                  </ListItem>
                  <ListItem>
                    <ListItemIcon>
                      <FitnessCenter sx={{ color: '#FF6B35' }} />
                    </ListItemIcon>
                    <ListItemText primary={`${selectedPackage.workoutCount} Antrenman Programı`} />
                  </ListItem>
                  {selectedPackage.includesNutrition && (
                    <ListItem>
                      <ListItemIcon>
                        <Restaurant sx={{ color: '#27AE60' }} />
                      </ListItemIcon>
                      <ListItemText primary="Kişisel Beslenme Planı" />
                    </ListItem>
                  )}
                  {selectedPackage.includesPersonalSupport && (
                    <ListItem>
                      <ListItemIcon>
                        <Support sx={{ color: '#9B59B6' }} />
                      </ListItemIcon>
                      <ListItemText primary="7/24 Kişisel Destek" />
                    </ListItem>
                  )}
                  {selectedPackage.includesTelegramAccess && (
                    <ListItem>
                      <ListItemIcon>
                        <Telegram sx={{ color: '#0088CC' }} />
                      </ListItemIcon>
                      <ListItemText primary="Özel Telegram Grubu" />
                    </ListItem>
                  )}
                </List>

                {/* Stats */}
                <Divider sx={{ my: 2 }} />
                <Grid container spacing={2}>
                  <Grid xs={6}>
                    <Paper sx={{ p: 2, textAlign: 'center' }}>
                      <Typography variant="h6" sx={{ color: '#FF6B35' }}>
                        {selectedPackage.soldCount}
                      </Typography>
                      <Typography variant="caption">
                        Toplam Satış
                      </Typography>
                    </Paper>
                  </Grid>
                  <Grid xs={6}>
                    <Paper sx={{ p: 2, textAlign: 'center' }}>
                      <Typography variant="h6" sx={{ color: '#27AE60' }}>
                        {selectedPackage.activeSubscriptions}
                      </Typography>
                      <Typography variant="caption">
                        Aktif Üye
                      </Typography>
                    </Paper>
                  </Grid>
                </Grid>
              </Box>
            </Box>
          )}
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button 
            startIcon={<Telegram />}
            onClick={() => window.open(selectedPackage?.telegramChannelLink, '_blank')}
            sx={{ color: '#0088CC' }}
          >
            Telegram Kanalı
          </Button>
          <Button onClick={() => setDetailDialogOpen(false)}>Kapat</Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default DevelopmentPackages;
