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
  Chip,
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
  CardContent
} from '@mui/material';
import {
  Timeline,
  TimelineItem,
  TimelineOppositeContent,
  TimelineSeparator,
  TimelineDot,
  TimelineConnector,
  TimelineContent
} from '@mui/lab';
import {
  LocalShipping,
  Edit,
  Visibility,
  CheckCircle,
  Cancel,
  Schedule,
  LocationOn,
  Assignment
} from '@mui/icons-material';

const AdminShipping = () => {
  const [orders, setOrders] = useState<any[]>([
    {
      id: 1,
      orderNumber: 'FRP-2024-001',
      customerName: 'Ahmet Yılmaz',
      customerPhone: '+90 532 123 45 67',
      address: 'Atatürk Mah. Cumhuriyet Cad. No: 123 Kadıköy/İstanbul',
      totalAmount: 549.98,
      shippingStatus: 'Shipped',
      orderDate: '2024-01-15',
      shippedDate: '2024-01-16',
      estimatedDelivery: '2024-01-18',
      trackingNumber: 'YK123456789',
      cargoCompany: 'Yurtiçi Kargo',
      cargoFee: 15.00,
      weight: 0.8,
      dimensions: '30x25x15 cm'
    },
    {
      id: 2,
      orderNumber: 'FRP-2024-002',
      customerName: 'Zeynep Kaya',
      customerPhone: '+90 555 987 65 43',
      address: 'Bahçelievler Mah. İnönü Sok. No: 45 Ankara',
      totalAmount: 849.97,
      shippingStatus: 'Processing',
      orderDate: '2024-01-16',
      cargoCompany: 'MNG Kargo',
      cargoFee: 20.00,
      weight: 1.2,
      dimensions: '35x30x20 cm'
    },
    {
      id: 3,
      orderNumber: 'FRP-2024-003',
      customerName: 'Mehmet Demir',
      customerPhone: '+90 543 111 22 33',
      address: 'Merkez Mah. Atatürk Bulv. No: 78 İzmir',
      totalAmount: 199.99,
      shippingStatus: 'Delivered',
      orderDate: '2024-01-14',
      shippedDate: '2024-01-15',
      deliveredDate: '2024-01-17',
      trackingNumber: 'PTT987654321',
      cargoCompany: 'PTT Kargo',
      cargoFee: 12.00,
      weight: 0.3,
      dimensions: '20x15x10 cm'
    }
  ]);

  const [selectedOrder, setSelectedOrder] = useState<any>(null);
  const [trackingDialogOpen, setTrackingDialogOpen] = useState(false);
  const [updateDialogOpen, setUpdateDialogOpen] = useState(false);
  const [newTrackingNumber, setNewTrackingNumber] = useState('');
  const [newCargoCompany, setNewCargoCompany] = useState('');
  const [newStatus, setNewStatus] = useState('');

  const cargoCompanies = [
    'Yurtiçi Kargo',
    'MNG Kargo',
    'PTT Kargo',
    'Aras Kargo',
    'Sürat Kargo',
    'UPS Kargo'
  ];

  const shippingStatuses = [
    { value: 'Pending', label: 'Beklemede', color: 'warning' },
    { value: 'Processing', label: 'Hazırlanıyor', color: 'info' },
    { value: 'Shipped', label: 'Kargoda', color: 'primary' },
    { value: 'Delivered', label: 'Teslim Edildi', color: 'success' },
    { value: 'Cancelled', label: 'İptal Edildi', color: 'error' }
  ];

  const handleViewTracking = (order: any) => {
    setSelectedOrder(order);
    setTrackingDialogOpen(true);
  };

  const handleUpdateShipping = (order: any) => {
    setSelectedOrder(order);
    setNewTrackingNumber(order.trackingNumber || '');
    setNewCargoCompany(order.cargoCompany || '');
    setNewStatus(order.shippingStatus);
    setUpdateDialogOpen(true);
  };

  const handleSaveUpdate = () => {
    if (selectedOrder) {
      setOrders(orders.map(order => 
        order.id === selectedOrder.id 
          ? {
              ...order,
              trackingNumber: newTrackingNumber,
              cargoCompany: newCargoCompany,
              shippingStatus: newStatus,
              shippedDate: newStatus === 'Shipped' && !order.shippedDate ? new Date().toISOString().split('T')[0] : order.shippedDate
            }
          : order
      ));
      setUpdateDialogOpen(false);
    }
  };

  const getStatusInfo = (status: string) => {
    return shippingStatuses.find(s => s.value === status) || { label: status, color: 'default' };
  };

  const getTrackingTimeline = (order: any) => {
    const timeline = [];
    
    timeline.push({
      date: order.orderDate,
      title: 'Sipariş Alındı',
      description: 'Siparişiniz başarıyla oluşturuldu',
      completed: true
    });

    if (order.shippingStatus !== 'Pending') {
      timeline.push({
        date: order.orderDate,
        title: 'Sipariş Hazırlanıyor',
        description: 'Ürünleriniz paketleniyor',
        completed: true
      });
    }

    if (order.shippedDate) {
      timeline.push({
        date: order.shippedDate,
        title: 'Kargoya Verildi',
        description: `${order.cargoCompany} - ${order.trackingNumber}`,
        completed: true
      });
    }

    if (order.estimatedDelivery && order.shippingStatus === 'Shipped') {
      timeline.push({
        date: order.estimatedDelivery,
        title: 'Tahmini Teslimat',
        description: 'Tahmini teslimat tarihi',
        completed: false
      });
    }

    if (order.deliveredDate) {
      timeline.push({
        date: order.deliveredDate,
        title: 'Teslim Edildi',
        description: 'Siparişiniz başarıyla teslim edildi',
        completed: true
      });
    }

    return timeline;
  };

  // İstatistikler
  const stats = [
    {
      title: 'Bekleyen Kargolar',
      value: orders.filter(o => o.shippingStatus === 'Processing').length,
      icon: <Schedule />,
      color: '#FFA500'
    },
    {
      title: 'Kargodaki Siparişler',
      value: orders.filter(o => o.shippingStatus === 'Shipped').length,
      icon: <LocalShipping />,
      color: '#3498DB'
    },
    {
      title: 'Teslim Edilenler',
      value: orders.filter(o => o.shippingStatus === 'Delivered').length,
      icon: <CheckCircle />,
      color: '#27AE60'
    },
    {
      title: 'Toplam Kargo Ücreti',
      value: `₺${orders.reduce((sum, o) => sum + (o.cargoFee || 0), 0).toFixed(2)}`,
      icon: <Assignment />,
      color: '#FF6B35'
    }
  ];

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      <Typography variant="h4" sx={{ mb: 4, fontWeight: 700, color: '#FF6B35' }}>
        🚚 Kargo Yönetimi
      </Typography>

      {/* İstatistikler */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        {stats.map((stat, index) => (
          <Grid xs={12} sm={6} md={3} key={index}>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
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
                    <Typography variant="h5" component="div">
                      {stat.value}
                    </Typography>
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

      {/* Kargo Tablosu */}
      <Paper sx={{ borderRadius: 3, overflow: 'hidden' }}>
        <Box sx={{ p: 3, bgcolor: '#F8F9FA', borderBottom: '1px solid #E0E0E0' }}>
          <Typography variant="h6" sx={{ fontWeight: 600 }}>
            Kargo Listesi
          </Typography>
        </Box>
        
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow sx={{ bgcolor: '#FFF5F0' }}>
                <TableCell sx={{ fontWeight: 600 }}>Sipariş</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Müşteri</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Kargo Bilgileri</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Durum</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Teslimat</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>İşlemler</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {orders.map((order) => (
                <TableRow key={order.id} sx={{ '&:hover': { bgcolor: '#FFFAF5' } }}>
                  <TableCell>
                    <Box>
                      <Typography variant="body2" sx={{ fontWeight: 600, color: '#FF6B35' }}>
                        {order.orderNumber}
                      </Typography>
                      <Typography variant="caption" color="text.secondary">
                        {new Date(order.orderDate).toLocaleDateString('tr-TR')}
                      </Typography>
                    </Box>
                  </TableCell>
                  <TableCell>
                    <Box>
                      <Typography variant="body2" sx={{ fontWeight: 500 }}>
                        {order.customerName}
                      </Typography>
                      <Typography variant="caption" color="text.secondary">
                        {order.customerPhone}
                      </Typography>
                    </Box>
                  </TableCell>
                  <TableCell>
                    {order.trackingNumber ? (
                      <Box>
                        <Typography variant="body2" sx={{ fontWeight: 500 }}>
                          {order.trackingNumber}
                        </Typography>
                        <Typography variant="caption" color="text.secondary">
                          {order.cargoCompany}
                        </Typography>
                        <Typography variant="caption" sx={{ display: 'block' }}>
                          {order.weight}kg - {order.dimensions}
                        </Typography>
                      </Box>
                    ) : (
                      <Typography variant="body2" color="text.secondary">
                        Henüz kargoya verilmedi
                      </Typography>
                    )}
                  </TableCell>
                  <TableCell>
                    <Chip 
                      label={getStatusInfo(order.shippingStatus).label}
                      color={getStatusInfo(order.shippingStatus).color as any}
                      size="small"
                      variant="outlined"
                    />
                  </TableCell>
                  <TableCell>
                    {order.deliveredDate ? (
                      <Typography variant="body2" color="success.main">
                        {new Date(order.deliveredDate).toLocaleDateString('tr-TR')}
                      </Typography>
                    ) : order.estimatedDelivery ? (
                      <Typography variant="body2" color="text.secondary">
                        Tahmini: {new Date(order.estimatedDelivery).toLocaleDateString('tr-TR')}
                      </Typography>
                    ) : (
                      <Typography variant="body2" color="text.secondary">
                        Belirlenmedi
                      </Typography>
                    )}
                  </TableCell>
                  <TableCell>
                    <Box sx={{ display: 'flex', gap: 1 }}>
                      <IconButton 
                        size="small" 
                        onClick={() => handleViewTracking(order)}
                        sx={{ color: '#FF6B35' }}
                      >
                        <Visibility />
                      </IconButton>
                      <IconButton 
                        size="small" 
                        onClick={() => handleUpdateShipping(order)}
                        sx={{ color: '#27AE60' }}
                      >
                        <Edit />
                      </IconButton>
                      <IconButton size="small" sx={{ color: '#3498DB' }}>
                        <LocationOn />
                      </IconButton>
                    </Box>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>

      {/* Takip Dialog */}
      <Dialog 
        open={trackingDialogOpen} 
        onClose={() => setTrackingDialogOpen(false)}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          Kargo Takibi - {selectedOrder?.orderNumber}
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          {selectedOrder && (
            <Box>
              <Grid container spacing={3} sx={{ mb: 3 }}>
                <Grid xs={12} md={6}>
                  <Typography variant="h6" gutterBottom>
                    Teslimat Adresi
                  </Typography>
                  <Typography variant="body2">
                    {selectedOrder.address}
                  </Typography>
                </Grid>
                <Grid xs={12} md={6}>
                  <Typography variant="h6" gutterBottom>
                    Kargo Bilgileri
                  </Typography>
                  <Typography variant="body2">
                    <strong>Şirket:</strong> {selectedOrder.cargoCompany || 'Belirlenmedi'}
                  </Typography>
                  <Typography variant="body2">
                    <strong>Takip No:</strong> {selectedOrder.trackingNumber || 'Belirlenmedi'}
                  </Typography>
                  <Typography variant="body2">
                    <strong>Ağırlık:</strong> {selectedOrder.weight}kg
                  </Typography>
                  <Typography variant="body2">
                    <strong>Boyutlar:</strong> {selectedOrder.dimensions}
                  </Typography>
                </Grid>
              </Grid>

              <Typography variant="h6" gutterBottom>
                Kargo Durumu
              </Typography>
              <Timeline position="left">
                {getTrackingTimeline(selectedOrder).map((item, index) => (
                  <TimelineItem key={index}>
                    <TimelineOppositeContent
                      sx={{ m: 'auto 0', fontSize: '0.875rem' }}
                      color="text.secondary"
                    >
                      {new Date(item.date).toLocaleDateString('tr-TR')}
                    </TimelineOppositeContent>
                    <TimelineSeparator>
                      <TimelineDot 
                        color={item.completed ? 'primary' : 'grey'}
                        sx={{ 
                          bgcolor: item.completed ? '#FF6B35' : '#E0E0E0' 
                        }}
                      />
                      {index < getTrackingTimeline(selectedOrder).length - 1 && <TimelineConnector />}
                    </TimelineSeparator>
                    <TimelineContent sx={{ py: '12px', px: 2 }}>
                      <Typography variant="h6" component="span">
                        {item.title}
                      </Typography>
                      <Typography color="text.secondary">
                        {item.description}
                      </Typography>
                    </TimelineContent>
                  </TimelineItem>
                ))}
              </Timeline>
            </Box>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setTrackingDialogOpen(false)}>Kapat</Button>
        </DialogActions>
      </Dialog>

      {/* Kargo Güncelleme Dialog */}
      <Dialog 
        open={updateDialogOpen} 
        onClose={() => setUpdateDialogOpen(false)}
        maxWidth="sm"
        fullWidth
      >
        <DialogTitle>Kargo Bilgilerini Güncelle</DialogTitle>
        <DialogContent sx={{ pt: 2 }}>
          <Grid container spacing={2}>
            <Grid xs={12}>
              <FormControl fullWidth>
                <InputLabel>Kargo Şirketi</InputLabel>
                <Select
                  value={newCargoCompany}
                  onChange={(e) => setNewCargoCompany(e.target.value)}
                  label="Kargo Şirketi"
                >
                  {cargoCompanies.map((company) => (
                    <MenuItem key={company} value={company}>
                      {company}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Takip Numarası"
                value={newTrackingNumber}
                onChange={(e) => setNewTrackingNumber(e.target.value)}
              />
            </Grid>
            <Grid xs={12}>
              <FormControl fullWidth>
                <InputLabel>Durum</InputLabel>
                <Select
                  value={newStatus}
                  onChange={(e) => setNewStatus(e.target.value)}
                  label="Durum"
                >
                  {shippingStatuses.map((status) => (
                    <MenuItem key={status.value} value={status.value}>
                      {status.label}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setUpdateDialogOpen(false)}>İptal</Button>
          <Button 
            variant="contained" 
            onClick={handleSaveUpdate}
            sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
          >
            Güncelle
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default AdminShipping;
