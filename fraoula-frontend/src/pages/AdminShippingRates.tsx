import React, { useState } from 'react';
import {
  Container,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  Box,
  Button,
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
  Grid,
  Card,
  CardContent,
  Switch,
  FormControlLabel,
  Chip
} from '@mui/material';
import {
  Add,
  Edit,
  Delete,
  LocalShipping,
  LocationCity,
  Calculate,
  Business,
  Schedule
} from '@mui/icons-material';

const AdminShippingRates = () => {
  const [rates, setRates] = useState([
    {
      id: "1",
      companyName: 'Yurtiçi Kargo',
      cityName: 'İstanbul',
      basePrice: 15.00,
      pricePerKg: 5.00,
      maxWeight: 30.0,
      estimatedDays: 1,
      isActive: true,
      notes: 'Hızlı teslimat'
    },
    {
      id: "2",
      companyName: 'MNG Kargo',
      cityName: 'Ankara',
      basePrice: 18.00,
      pricePerKg: 6.00,
      maxWeight: 25.0,
      estimatedDays: 2,
      isActive: true,
      notes: 'Güvenli teslimat'
    },
    {
      id: "3",
      companyName: 'PTT Kargo',
      cityName: 'İzmir',
      basePrice: 12.00,
      pricePerKg: 4.50,
      maxWeight: 20.0,
      estimatedDays: 3,
      isActive: true,
      notes: 'Ekonomik seçenek'
    }
  ]);

  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingRate, setEditingRate] = useState<any>(null);
  const [calculatorOpen, setCalculatorOpen] = useState(false);
  
  const [formData, setFormData] = useState({
    companyName: '',
    cityName: '',
    basePrice: 0,
    pricePerKg: 0,
    maxWeight: 0,
    estimatedDays: 1,
    isActive: true,
    notes: ''
  });

  const [calculator, setCalculator] = useState({
    cityName: '',
    companyName: '',
    weight: 0,
    calculatedFee: 0
  });

  const companies = [
    'Yurtiçi Kargo',
    'MNG Kargo', 
    'PTT Kargo',
    'Aras Kargo',
    'Sürat Kargo',
    'UPS Kargo'
  ];

  const cities = [
    'İstanbul', 'Ankara', 'İzmir', 'Bursa', 'Antalya', 'Adana', 'Konya', 'Gaziantep',
    'Şanlıurfa', 'Kocaeli', 'Mersin', 'Diyarbakır', 'Hatay', 'Manisa', 'Kayseri'
  ];

  const handleEdit = (rate: any) => {
    setEditingRate(rate);
    setFormData({
      companyName: rate.companyName,
      cityName: rate.cityName,
      basePrice: rate.basePrice,
      pricePerKg: rate.pricePerKg,
      maxWeight: rate.maxWeight,
      estimatedDays: rate.estimatedDays,
      isActive: rate.isActive,
      notes: rate.notes || ''
    });
    setDialogOpen(true);
  };

  const handleCreate = () => {
    setEditingRate(null);
    setFormData({
      companyName: '',
      cityName: '',
      basePrice: 0,
      pricePerKg: 0,
      maxWeight: 0,
      estimatedDays: 1,
      isActive: true,
      notes: ''
    });
    setDialogOpen(true);
  };

  const handleSave = () => {
    if (editingRate) {
      setRates(rates.map(r => 
        r.id === editingRate.id 
          ? { ...r, ...formData }
          : r
      ));
    } else {
      const newRate = {
        id: (Math.max(...rates.map(r => parseInt(r.id))) + 1).toString(),
        ...formData
      };
      setRates([...rates, newRate]);
    }
    setDialogOpen(false);
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Bu kargo ücretini silmek istediğinizden emin misiniz?')) {
      setRates(rates.filter(r => r.id !== id));
    }
  };

  const calculateFee = () => {
    const rate = rates.find(r => 
      r.cityName === calculator.cityName && 
      r.companyName === calculator.companyName &&
      r.isActive &&
      calculator.weight <= r.maxWeight
    );

    if (rate) {
      const fee = rate.basePrice + (rate.pricePerKg * calculator.weight);
      setCalculator({ ...calculator, calculatedFee: fee });
    } else {
      alert('Bu şehir ve kargo şirketi kombinasyonu için ücret bulunamadı!');
      setCalculator({ ...calculator, calculatedFee: 0 });
    }
  };

  const stats = [
    {
      title: 'Toplam Kargo Şirketi',
      value: new Set(rates.map(r => r.companyName)).size,
      icon: <Business />,
      color: '#FF6B35'
    },
    {
      title: 'Aktif Şehir',
      value: new Set(rates.map(r => r.cityName)).size,
      icon: <LocationCity />,
      color: '#27AE60'
    },
    {
      title: 'Ortalama Teslimat',
      value: `${(rates.reduce((sum, r) => sum + r.estimatedDays, 0) / rates.length).toFixed(1)} gün`,
      icon: <Schedule />,
      color: '#3498DB'
    },
    {
      title: 'Min Kargo Ücreti',
      value: `₺${Math.min(...rates.map(r => r.basePrice)).toFixed(2)}`,
      icon: <LocalShipping />,
      color: '#9B59B6'
    }
  ];

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 4 }}>
        <Typography variant="h4" sx={{ fontWeight: 700, color: '#FF6B35' }}>
          🚚 Kargo Ücret Yönetimi
        </Typography>
        <Box sx={{ display: 'flex', gap: 2 }}>
          <Button
            variant="outlined"
            startIcon={<Calculate />}
            onClick={() => setCalculatorOpen(true)}
            sx={{ borderColor: '#FF6B35', color: '#FF6B35' }}
          >
            Ücret Hesapla
          </Button>
          <Button
            variant="contained"
            startIcon={<Add />}
            onClick={handleCreate}
            sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
          >
            Yeni Ücret Ekle
          </Button>
        </Box>
      </Box>

      {/* İstatistikler */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        {stats.map((stat, index) => (
          <Grid xs={12} sm={6} md={3} key={index}>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <Box
                    sx={{
                      bgcolor: stat.color,
                      color: 'white',
                      borderRadius: 2,
                      p: 1,
                      mr: 2
                    }}
                  >
                    {stat.icon}
                  </Box>
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

      {/* Kargo Ücreti Tablosu */}
      <Paper sx={{ borderRadius: 3, overflow: 'hidden' }}>
        <Box sx={{ p: 3, bgcolor: '#F8F9FA', borderBottom: '1px solid #E0E0E0' }}>
          <Typography variant="h6" sx={{ fontWeight: 600 }}>
            Kargo Ücret Listesi
          </Typography>
        </Box>
        
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow sx={{ bgcolor: '#FFF5F0' }}>
                <TableCell sx={{ fontWeight: 600 }}>Kargo Şirketi</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Şehir</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Temel Ücret</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Kg Başına</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Max Ağırlık</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Teslimat</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Durum</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>İşlemler</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rates.map((rate) => (
                <TableRow key={rate.id} sx={{ '&:hover': { bgcolor: '#FFFAF5' } }}>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <LocalShipping sx={{ color: '#FF6B35', mr: 1 }} />
                      <Typography variant="body2" sx={{ fontWeight: 600 }}>
                        {rate.companyName}
                      </Typography>
                    </Box>
                  </TableCell>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <LocationCity sx={{ color: '#27AE60', mr: 1 }} />
                      {rate.cityName}
                    </Box>
                  </TableCell>
                  <TableCell>
                    <Typography variant="body2" sx={{ fontWeight: 600, color: '#FF6B35' }}>
                      ₺{rate.basePrice.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Typography variant="body2">
                      ₺{rate.pricePerKg.toFixed(2)}/kg
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Typography variant="body2">
                      {rate.maxWeight}kg
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Chip 
                      label={`${rate.estimatedDays} gün`}
                      size="small"
                      sx={{ bgcolor: '#E3F2FD', color: '#1976D2' }}
                    />
                  </TableCell>
                  <TableCell>
                    <Chip 
                      label={rate.isActive ? 'Aktif' : 'Pasif'}
                      color={rate.isActive ? 'success' : 'error'}
                      size="small"
                      variant="outlined"
                    />
                  </TableCell>
                  <TableCell>
                    <Box sx={{ display: 'flex', gap: 1 }}>
                      <IconButton 
                        size="small" 
                        sx={{ color: '#27AE60' }}
                        onClick={() => handleEdit(rate)}
                      >
                        <Edit />
                      </IconButton>
                      <IconButton 
                        size="small" 
                        sx={{ color: '#E74C3C' }}
                        onClick={() => handleDelete(rate.id)}
                      >
                        <Delete />
                      </IconButton>
                    </Box>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>

      {/* Ekleme/Düzenleme Dialog */}
      <Dialog open={dialogOpen} onClose={() => setDialogOpen(false)} maxWidth="md" fullWidth>
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          {editingRate ? 'Kargo Ücreti Düzenle' : 'Yeni Kargo Ücreti Ekle'}
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          <Grid container spacing={3} sx={{ mt: 1 }}>
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Kargo Şirketi</InputLabel>
                <Select
                  value={formData.companyName}
                  onChange={(e) => setFormData({...formData, companyName: e.target.value})}
                  label="Kargo Şirketi"
                >
                  {companies.map((company) => (
                    <MenuItem key={company} value={company}>{company}</MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Şehir</InputLabel>
                <Select
                  value={formData.cityName}
                  onChange={(e) => setFormData({...formData, cityName: e.target.value})}
                  label="Şehir"
                >
                  {cities.map((city) => (
                    <MenuItem key={city} value={city}>{city}</MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Temel Ücret (₺)"
                type="number"
                value={formData.basePrice}
                onChange={(e) => setFormData({...formData, basePrice: parseFloat(e.target.value)})}
              />
            </Grid>
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Kg Başına Ücret (₺)"
                type="number"
                value={formData.pricePerKg}
                onChange={(e) => setFormData({...formData, pricePerKg: parseFloat(e.target.value)})}
              />
            </Grid>
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Maksimum Ağırlık (kg)"
                type="number"
                value={formData.maxWeight}
                onChange={(e) => setFormData({...formData, maxWeight: parseFloat(e.target.value)})}
              />
            </Grid>
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Tahmini Teslimat (gün)"
                type="number"
                value={formData.estimatedDays}
                onChange={(e) => setFormData({...formData, estimatedDays: parseInt(e.target.value)})}
              />
            </Grid>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Notlar"
                multiline
                rows={2}
                value={formData.notes}
                onChange={(e) => setFormData({...formData, notes: e.target.value})}
              />
            </Grid>
            <Grid xs={12}>
              <FormControlLabel
                control={
                  <Switch
                    checked={formData.isActive}
                    onChange={(e) => setFormData({...formData, isActive: e.target.checked})}
                  />
                }
                label="Aktif"
              />
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setDialogOpen(false)}>İptal</Button>
          <Button 
            variant="contained" 
            onClick={handleSave}
            sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
          >
            {editingRate ? 'Güncelle' : 'Oluştur'}
          </Button>
        </DialogActions>
      </Dialog>

      {/* Kargo Ücreti Hesaplama Dialog */}
      <Dialog open={calculatorOpen} onClose={() => setCalculatorOpen(false)} maxWidth="sm" fullWidth>
        <DialogTitle sx={{ bgcolor: '#27AE60', color: 'white' }}>
          Kargo Ücreti Hesaplama
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          <Grid container spacing={3} sx={{ mt: 1 }}>
            <Grid xs={12}>
              <FormControl fullWidth>
                <InputLabel>Şehir</InputLabel>
                <Select
                  value={calculator.cityName}
                  onChange={(e) => setCalculator({...calculator, cityName: e.target.value})}
                  label="Şehir"
                >
                  {cities.map((city) => (
                    <MenuItem key={city} value={city}>{city}</MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12}>
              <FormControl fullWidth>
                <InputLabel>Kargo Şirketi</InputLabel>
                <Select
                  value={calculator.companyName}
                  onChange={(e) => setCalculator({...calculator, companyName: e.target.value})}
                  label="Kargo Şirketi"
                >
                  {companies.map((company) => (
                    <MenuItem key={company} value={company}>{company}</MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Ağırlık (kg)"
                type="number"
                value={calculator.weight}
                onChange={(e) => setCalculator({...calculator, weight: parseFloat(e.target.value)})}
              />
            </Grid>
            {calculator.calculatedFee > 0 && (
              <Grid xs={12}>
                <Paper sx={{ p: 2, bgcolor: '#E8F5E8', textAlign: 'center' }}>
                  <Typography variant="h5" sx={{ color: '#27AE60', fontWeight: 700 }}>
                    Kargo Ücreti: ₺{calculator.calculatedFee.toFixed(2)}
                  </Typography>
                </Paper>
              </Grid>
            )}
          </Grid>
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setCalculatorOpen(false)}>Kapat</Button>
          <Button 
            variant="contained" 
            onClick={calculateFee}
            sx={{ bgcolor: '#27AE60', '&:hover': { bgcolor: '#219A52' } }}
          >
            Hesapla
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default AdminShippingRates;
